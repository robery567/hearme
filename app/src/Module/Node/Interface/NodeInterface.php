<?php

interface Module_Node_Interface_NodeInterface {
    /**
     * @var bool
     */
    const COLOR_BLACK = true;

    /**
     * @var bool
     */
    const COLOR_RED = false;

    /**
     * Children position
     *
     * @var integer
     */
    const POSITION_LEFT = -1;

    /**
     * Children position
     *
     * @var integer
     */
    const POSITION_RIGHT = 1;

    /**
     * Children position
     *
     * @var null
     */
    const POSITION_ROOT = null;

    /**
     * Id can be anything. Basically you will want to use it with integer.
     *
     * @return mixed
     */
    public function getId();

    /**
     * @param mixed $id
     * @return $this
     */
    public function setId($id);

    /**
     * @return mixed
     */
    public function getValue();

    /**
     * @param mixed $value
     * @return $this
     */
    public function setValue($value);

    /**
     * @return boolean
     */
    public function getColor();

    /**
     * @param boolean $color
     * @return $this
     */
    public function setColor($color);

    /**
     * @return integer
     */
    public function getPosition();

    /**
     * Position can be null if root
     *
     * @param integer|null $position
     * @return $this
     */
    public function setPosition($position);

    /**
     * @return Module_Node_Model|null
     */
    public function getParent();

    /**
     * If the parent is null, it's a root
     *
     * @param Module_Node_Model|null $parent
     * @return $this
     */
    public function setParent(Module_Node_Model $parent = null);

    /**
     * @param int $position
     * @return Module_Node_Model
     */
    public function getChild($position);

    /**
     * Set child
     *
     * @param int $position
     * @param Module_Node_Model|null $child
     * @return $this
     */
    public function setChild($position, Module_Node_Model $child = null);

    /**
     * @param int $position
     * @return bool
     */
    public function haveChild($position);

    /**
     * get grand parent if a parent exist
     *
     * @return Module_Node_Model|null
     */
    public function getGrandParent();

    /**
     * Get uncle if grand parent exist
     *
     * @return Module_Node_Model|null
     */
    public function getUncle();

    /**
     * Is a NIL / Leaf
     * A node without child is a leaf (so with both entry as null)
     *
     * @return bool
     */
    public function isLeaf();
}
