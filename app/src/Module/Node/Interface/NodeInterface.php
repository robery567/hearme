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
    function getId();

    /**
     * @param mixed $id
     * @return $this
     */
    function setId($id);

    /**
     * @return mixed
     */
    function getValue();

    /**
     * @param mixed $value
     * @return $this
     */
    function setValue($value);

    /**
     * @return boolean
     */
    function getColor();

    /**
     * @param boolean $color
     * @return $this
     */
    function setColor($color);

    /**
     * @return integer
     */
    function getPosition();

    /**
     * Position can be null if root
     *
     * @param integer|null $position
     * @return $this
     */
    function setPosition($position);

    /**
     * @return Module_Node_Model|null
     */
    function getParent();

    /**
     * If the parent is null, it's a root
     *
     * @param Module_Node_Model|null $parent
     * @return $this
     */
    function setParent(Module_Node_Model $parent = null);

    /**
     * @param int $position
     * @return Module_Node_Model
     */
    function getChild($position);

    /**
     * Set child
     *
     * @param int $position
     * @param Module_Node_Model|null $child
     * @return $this
     */
    function setChild($position, Module_Node_Model $child = null);

    /**
     * @param int $position
     * @return bool
     */
    function haveChild($position);

    /**
     * get grand parent if a parent exist
     *
     * @return Module_Node_Model|null
     */
    function getGrandParent();

    /**
     * Get uncle if grand parent exist
     *
     * @return Module_Node_Model|null
     */
    function getUncle();

    /**
     * Is a NIL / Leaf
     * A node without child is a leaf (so with both entry as null)
     *
     * @return bool
     */
    function isLeaf();
}
