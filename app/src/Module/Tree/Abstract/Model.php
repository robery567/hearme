<?php

/**
 * Class Module_Tree_Abstract_Model
 */
abstract class Module_Tree_Abstract_Model implements Module_Tree_Interface_TreeInterface {
    /**
     * Root node of the tree
     *
     * @access protected
     * @var Module_Node_Model
     */
    protected $root;

    /**
     * @param Module_Node_Model|null $node
     */
    protected function setRoot(Module_Node_Model $node = null) {
        $this->root = null !== $node ? $node->setPosition(null)->setParent(null) : null;
    }

    /**
     * @return Module_Node_Model
     */
    public function getRoot() {
        return $this->root;
    }

    /**
     * @param Module_Node_Model|null $node
     */
    public function __construct(Module_Node_Model $node = null) {
        $this->setRoot($node);
    }

    /**
     * Insert a node
     *
     * @param  Module_Node_Model $node
     * @return $this
     * @throws Exception
     */
    public function insert(Module_Node_Model $node) {
        // If root is null, set as root
        if (null === $this->root) {
            $this->setRoot($node);
            // Else Find the new parent
        } else {
            $insertNode = $this->searchClosestNode($this->root, $node->getId());
            $insertNode->setChild($this->compare($insertNode->getId(), $node->getId()), $node);
        }

        // New node is red
        $node->setColor(Module_Node_Model::COLOR_RED);

        $this->insertSort($node);
        $this->root->setColor(Module_Node_Model::COLOR_BLACK);

        return $this;
    }

    /**
     * @param Module_Node_Model $node
     * @return $this
     */
    protected function insertSort(Module_Node_Model $node) {

        if (null === $node->getParent()) {
            $node->setColor(Module_Node_Model::COLOR_BLACK);
            return $this;
        }

        if (!Module_Node_Model::COLOR_RED === $node->getParent()->getColor()
            && Module_Node_Model::COLOR_RED === $node->getColor()
        ) {
            return $this;
        }

        $grandParent = $node->getGrandParent();
        $uncle = $node->getUncle();

        if ($node->getPosition() === -$node->getParent()->getPosition()) {
            if (null === $uncle || Module_Node_Model::COLOR_BLACK === $uncle->getColor()) {
                $parent = $node->getParent();

                $this->rotate($node->getParent(), -$node->getPosition());

                return $this->insertSort($parent);
            } else {
                $uncle->setColor(Module_Node_Model::COLOR_BLACK);
                $node->getParent()->setColor(Module_Node_Model::COLOR_BLACK);

                if (null !== $grandParent) {
                    $grandParent->setColor(Module_Node_Model::COLOR_RED);
                }

                return $this->insertSort($grandParent);
            }
            // Second case, both parent and grand parent are on the same side
            // Rotate the parent to grand parent place
        } else {
            // If uncle and parent are red, set both black
            if (null !== $uncle && Module_Node_Model::COLOR_RED === $uncle->getColor()
                && Module_Node_Model::COLOR_RED === $node->getParent()->getColor()
            ) {
                $uncle->setColor(Module_Node_Model::COLOR_BLACK);
                $node->getParent()->setColor(Module_Node_Model::COLOR_BLACK);
                $grandParent->setColor(Module_Node_Model::COLOR_RED);

                return $this->insertSort($grandParent);
                // Else if we have a grand parent (so the same direction as parent)
            } elseif (null !== $grandParent) {
                $node->getParent()->setColor(Module_Node_Model::COLOR_BLACK);
                $grandParent->setColor(Module_Node_Model::COLOR_RED);
                $this->rotate($grandParent, -$node->getPosition());

                return $this->insertSort($grandParent);
            }
        }

        // Elsewhere obviously there is no problem
        return $this;
    }

    /**
     * Recursive search of the parent node of $node in $hierarchy
     * First call must be with $this->root or course.
     *
     * @param Module_Node_Model $hierarchy
     * @param int $id
     * @return Module_Node_Model
     * @throws \Exception
     */
    protected function searchClosestNode(Module_Node_Model $hierarchy, $id) {
        $position = $this->compare($hierarchy->getId(), $id);

        if ($hierarchy->isLeaf() || 0 === $position || !$hierarchy->haveChild($position)) {
            return $hierarchy;
        }

        return $this->searchClosestNode($hierarchy->getChild($position), $id);
    }

    /**
     * Do a rotation with node's parent
     *
     * @param Module_Node_Model $node
     * @param int $toPosition
     * @return $this
     */
    protected function rotate(Module_Node_Model $node, $toPosition) {
        // The new child of node in $toPosition
        $tmp = $node->getChild(-$toPosition);
        // Set node's child the grand son of son
        $node->setChild(-$toPosition, $tmp->getChild($toPosition));

        if ($tmp->haveChild($toPosition)) {
            $tmp->getChild($toPosition)->setParent($node);
        }

        $tmp->setParent($node->getParent());

        // If it's not the root, set parent's child
        if (null !== $node->getParent()) {
            $node->getParent()->setChild(($toPosition === $node->getPosition() ? 1 : -1) * $toPosition, $tmp);
        }

        $tmp->setChild($toPosition, $node);
        $node->setParent($tmp);

        // Rotation done, it's possible the root have changed
        if (null === $tmp->getParent()) {
            $this->setRoot($tmp);
        }

        return $this;
    }

    /**
     * Find a node by id.
     * Recursive operation, the optional $node should not be given
     *
     * @param int|string|array $keyVal
     * @param Module_Node_Model|null $node
     * @param string|array $searchByKey
     * @return false|Module_Node_Model
     * @throws Exception
     */
    public function find($keyVal, Module_Node_Model $node = null, $searchByKey = 'id') {
        // Initialize if first iteration
        if (null === $node) {
            $node = $this->root;
        }

        if ((is_array($keyVal) && !is_array($searchByKey)) || (is_array($searchByKey) && !is_array($keyVal))) {
            throw new Exception('Both search parameters should be arrays');
        }

        if ($searchByKey === 'id') {
            // If the id is equal, it's our match !
            $position = $this->compare($node->getId(), $keyVal);
            if (0 === $position) {
                return $node;
            }

            // Else if it's a nil, return false, else recursion
            return $node->isLeaf() ? false : $this->find($keyVal, $node->getChild($position), $searchByKey);
        } else if (is_array($keyVal) && is_array($searchByKey)) {
            $found = true;

            foreach($searchByKey as $keyId => $keyName) {
                if (empty($node->getValue()[$keyName]) || $node->getValue()[$keyName] !== $keyVal[$keyId]) {
                    $found = false;
                    echo $node->getValue()[$keyName] . "/" . $keyVal[$keyId] . "<br/>";
                    break;
                }
            }

            if ($found === true) {
                return $node;
            }

            return $node->isLeaf() ? null : $this->find($keyVal, $node->getChild(1), $searchByKey);
        } else {
            if (!empty($node->getValue()[$searchByKey]) && $node->getValue()[$searchByKey] === $keyVal) {
                return $node;
            }

            return $node->isLeaf() ? null : $this->find($keyVal, $node->getChild(1), $searchByKey);
        }
    }

    /**
     * @param Module_Node_Model $node
     * @param int $position
     * @param Module_Node_Model|null $relative
     * @return Module_Node_Model
     */
    public function findRelative(Module_Node_Model $node, $position, Module_Node_Model $relative = null) {
        // If we have already seek deeper and found a leaf, return the leaf
        if (null !== $relative && $node->isLeaf()) {
            return $node;
        }

        // If we have a child at this position, go seek to this child opposite direction until find a leaf
        if ($node->haveChild($position)) {
            // If it's a deep search, don't rotate search order. The closest is the deepest
            return $this->findRelative($node->getChild($position), (null === $relative ? -1 : 1) * $position, $node);
        }

        // It's the parent if parent direction is the same as node, else it's grand parent
        return $node->getPosition() === -$position ? $node->getParent() : $node->getGrandParent();
    }

    /**
     * Alias method
     *
     * @param Module_Node_Model $node
     * @return Module_Node_Model
     */
    public function findPredecessor(Module_Node_Model $node) {
        return $this->findRelative($node, Module_Node_Model::POSITION_LEFT);
    }

    /**
     * Alias method
     *
     * @param Module_Node_Model $node
     * @return Module_Node_Model
     */
    public function findSuccessor(Module_Node_Model $node) {
        return $this->findRelative($node, Module_Node_Model::POSITION_RIGHT);
    }

    /**
     * Remove the node from the tree.
     * The removed node will be orphan (no child nor parent).
     *
     * @param Module_Node_Model $node
     * @return $this
     */
    public function remove(Module_Node_Model $node) {
        if (!$node->haveChild(Module_Node_Model::POSITION_LEFT) || !$node->haveChild(Module_Node_Model::POSITION_RIGHT)) {
            $tmp = $node;
        } else {
            $tmp = $this->findRelative($node, null === $node->getPosition() ?
                Module_Node_Model::POSITION_RIGHT : $node->getPosition()
            );
        }

        $alt = $tmp->getChild(Module_Node_Model::POSITION_LEFT) ?: $tmp->getChild(Module_Node_Model::POSITION_RIGHT);

        if (null === $alt) {
            $alt = $tmp;
        }

        $alt->setParent($tmp->getParent());

        if (null === $node->getParent()) {
            $this->root = $alt;
        } elseif (null !== $tmp->getParent()) {
            $tmp->getParent()->setChild($tmp->getPosition(), $alt);
        }

        if ($tmp !== $node) {
            if ($tmp !== $alt && Module_Node_Model::COLOR_BLACK === $tmp->getColor()) {
                $this->deleteSort($alt);
            }

            if (null !== $tmp->getParent()) {
                $tmp->getParent()->setChild($tmp->getPosition(), $tmp->getChild($tmp->getPosition()));
            }

            $tmp
                ->setChild(Module_Node_Model::POSITION_LEFT, $node->getChild(Module_Node_Model::POSITION_LEFT))
                ->setChild(Module_Node_Model::POSITION_RIGHT, $node->getChild(Module_Node_Model::POSITION_RIGHT))
                ->setParent($node->getParent())
                ->setColor($node->getColor());

            if ($node->haveChild(Module_Node_Model::POSITION_LEFT)) {
                $node->getChild(Module_Node_Model::POSITION_LEFT)->setParent($tmp);
            }
            if ($node->haveChild(Module_Node_Model::POSITION_RIGHT)) {
                $node->getChild(Module_Node_Model::POSITION_RIGHT)->setParent($tmp);
            }

            if (null !== $node->getParent()) {
                $node->getParent()->setChild($node->getPosition(), $tmp);
            }
        } else {
            if (Module_Node_Model::COLOR_BLACK === $node->getColor()) {
                $this->deleteSort($node);
            }
        }

        // Make original node orphan
        if (null !== $node->getParent() && $node === $node->getParent()->getChild($node->getPosition())) {
            $node->getParent()->setChild($node->getPosition(), null);
        }

        $node
            ->setPosition(null)
            ->setParent(null)
            ->setChild(Module_Node_Model::POSITION_LEFT, null)
            ->setChild(Module_Node_Model::POSITION_RIGHT, null);

        return $this;
    }

    /**
     * Do rotations on related places on deletion
     *
     * @param Module_Node_Model $node
     * @return $this
     */
    protected function deleteSort(Module_Node_Model $node) {
        // If is root or black, go back
        if (Module_Node_Model::COLOR_BLACK !== $node->getColor() || null === $node->getParent()) {
            return $this;
        }

        $direction = $node->getPosition();
        $tmp = $node->getParent()->getChild(-$direction);

        if (null !== $tmp) {
            if (Module_Node_Model::COLOR_RED === $tmp->getColor()) {
                $tmp->setColor(Module_Node_Model::COLOR_BLACK);
                $node->getParent()->setColor(Module_Node_Model::COLOR_RED);
                $this->rotate($node->getParent(), $direction);
                $tmp = $node->getParent()->getChild(-$direction);
            }

            if ($tmp->haveChild(Module_Node_Model::POSITION_LEFT) && $tmp->getChild(Module_Node_Model::POSITION_LEFT)->getColor() === Module_Node_Model::COLOR_BLACK
                && $tmp->haveChild(Module_Node_Model::POSITION_RIGHT) && $tmp->getChild(Module_Node_Model::POSITION_RIGHT)->getColor() === Module_Node_Model::COLOR_BLACK
            ) {
                $tmp->setColor(Module_Node_Model::COLOR_RED);
                return $this->deleteSort($node->getParent());
            } else {
                if ($tmp->haveChild(-$direction) && $tmp->getChild(-$direction)->getColor() === Module_Node_Model::COLOR_BLACK) {
                    if ($tmp->haveChild($direction)) {
                        $tmp->getChild($direction)->setColor(Module_Node_Model::COLOR_BLACK);
                    }
                    $tmp->setColor(Module_Node_Model::COLOR_RED);
                    $this->rotate($tmp, -$direction);
                    $tmp = $node->getParent()->getChild(-$direction);
                }

                $tmp->setColor($node->getParent()->getColor());
                $node->getParent()->setColor(Module_Node_Model::COLOR_BLACK);
                if ($tmp->getChild(-$direction)) {
                    $tmp->getChild(-$direction)->setColor(Module_Node_Model::COLOR_BLACK);
                }
                $this->rotate($node->getParent(), $direction);
                $node = $this->root;
            }
        }

        return $this->deleteSort($node->setColor(Module_Node_Model::COLOR_BLACK));
    }

    /**
     * Get nodes with id between min and max (inclusive)
     *
     * @param int $min
     * @param int $max
     * @return array|Module_Node_Model[]
     * @throws Exception
     */
    public function enumerate($min, $max) {
        $out = [];
        $closest = $this->searchClosestNode($this->root, $min);
        // Include the first match only if above or equal to $min
        if ($this->compare($min, $closest->getId()) >= 0) {
            $out[] = $closest;
        }

        while (true) {
            // Get next node after $closest
            $nextNode = $this->findRelative($closest, Module_Node_Model::POSITION_RIGHT);
            if (null !== $nextNode // Not an end
                && $this->compare($max, $nextNode->getId()) <= 0 // Must be above
                && 0 != $this->compare($closest->getId(), $nextNode->getId()) // Not equal, should not happen
            ) {
                $out[] = $nextNode;
            } else {
                break;
            }

            $closest = $nextNode;
        }

        return $out;
    }

    /**
     * Return all Nodes in infixe ordered list.
     * The optional node argument is the starting point.
     * In most case it should be empty.
     *
     * @param Module_Node_Model|null $node
     * @return array|Module_Node_Model[]
     */
    public function infixeList(Module_Node_Model $node = null) {
        if (null === $node) {
            $node = $this->root;
        }

        $out = [];

        if (null !== $node && $node->haveChild(Module_Node_Model::POSITION_LEFT)) {
            $out = array_merge($out, $this->infixeList($node->getChild(Module_Node_Model::POSITION_LEFT)));
        }

        $out[] = $node;

        if (null !== $node && $node->haveChild(Module_Node_Model::POSITION_RIGHT)) {
            $out = array_merge($out, $this->infixeList($node->getChild(Module_Node_Model::POSITION_RIGHT)));
        }

        return $out;
    }

    /**
     * Compare two ids
     * If A is bellow B, return 1
     * If A is above B, return -1
     * If A is equal to B, return 0
     * It for syntax reason.
     *
     * @param mixed $idA
     * @param mixed $idB
     * @return bool
     */
    protected abstract function compare($idA, $idB);
}
