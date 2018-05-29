<?php

/**
 * Class Module_Node_Abstract_Model
 */
abstract class Module_Node_Abstract_Model implements Module_Node_Interface_NodeInterface {
    /**
     * @var int
     */
    protected $id;

    /**
     * @var mixed
     */
    protected $value;

    /**
     * @var bool
     */
    protected $color = self::COLOR_BLACK;

    /**
     * @var array|Module_Node_Model[]
     */
    protected $children = [
        self::POSITION_LEFT => null,
        self::POSITION_RIGHT => null,
    ];

    /**
     * Node's position relative to parent
     *
     * @var bool
     */
    protected $position;

    /**
     * Parent of the node
     *
     * @var Module_Node_Model
     */
    protected $parent;

    /**
     * @param int $id
     * @param mixed $value
     */
    public function __construct($id, $value) {
        $this
            ->setId($id)
            ->setValue($value);
    }

    abstract public function __toString();

    /**
     * @return mixed
     */
    abstract public function getId();

    /**
     * @param mixed $id
     * @return $this
     */
    abstract public function setId($id);

    /**
     * @return mixed
     */
    abstract public function getValue();

    /**
     * @param mixed $value
     * @return $this
     */
    abstract public function setValue($value);

    /**
     * @return boolean
     */
    public function getColor() {
        return $this->color;
    }

    /**
     * @param boolean $color
     * @return $this
     */
    public function setColor($color) {
        $this->color = $color;
        return $this;
    }

    /**
     * @param int $position
     * @return Module_Node_Model
     */
    public function getChild($position) {
        return $this->children[$position];
    }

    /**
     * @param int $position
     * @return bool
     */
    public function haveChild($position) {
        return null !== $this->children[$position];
    }

    /**
     * Set child
     *
     * @param int $position
     * @param Module_Node_Model|null $child
     * @return $this
     */
    public function setChild($position, Module_Node_Model $child = null) {
        $this->children[$position] = $child;

        if (null !== $child) {
            $child->setParent($this)->setPosition($position);
        }

        return $this;
    }

    /**
     * @return integer
     */
    public function getPosition() {
        return $this->position;
    }

    /**
     * @param integer|null $position
     * @return $this
     */
    public function setPosition($position) {
        $this->position = $position;

        return $this;
    }

    /**
     * @return Module_Node_Model|null
     */
    public function getParent() {
        return $this->parent;
    }

    /**
     * @param Module_Node_Model|null $parent
     * @return $this
     */
    public function setParent(Module_Node_Model $parent = null) {
        $this->parent = $parent;

        return $this;
    }

    /**
     * get grand parent if a parent exist
     *
     * @return Module_Node_Model|null
     */
    public function getGrandParent() {
        if (null !== $this->getParent()) {
            return $this->getParent()->getParent();
        }

        return null;
    }

    /**
     * Get uncle if grand parent exist
     *
     * @return Module_Node_Model|null
     */
    public function getUncle() {
        if (null !== $this->getGrandParent()) {
            return $this->getGrandParent()->getChild(-$this->getParent()->getPosition());
        }

        return null;
    }

    /**
     * Is a NIL / Leaf
     *
     * @return bool
     */
    public function isLeaf() {
        return null === $this->children[static::POSITION_LEFT]
            && null === $this->children[static::POSITION_RIGHT];
    }
}
