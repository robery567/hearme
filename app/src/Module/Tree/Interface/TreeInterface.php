<?php

interface Module_Tree_Interface_TreeInterface {
    /**
     * Should call a root setter to setup node parent and position
     *
     * @param Module_Node_Model|null $node
     */
    function __construct(Module_Node_Model $node = null);

    /**
     * @return Module_Node_Model
     */
    function getRoot();

    /**
     * Insert a node a perform rotations if needed
     *
     * @param Module_Node_Model $node
     * @return $this
     */
    function insert(Module_Node_Model $node);

    /**
     * Remove the node from the tree.
     * The removed node will be orphan (no child nor parent).
     *
     * @param Module_Node_Model $node
     * @return $this
     */
    function remove(Module_Node_Model $node);

    /**
     * Find the node with the given id
     *
     * @param mixed $id
     * @return mixed
     */
    function find($id);

    /**
     * Find the predecessor (left) or successor (right).
     * If none exist, return given node
     *
     * @param Module_Node_Model $node
     * @param int $position
     * @return Module_Node_Model
     */
    function findRelative(Module_Node_Model $node, $position);

    /**
     * @param Module_Node_Model $node
     * @return Module_Node_Model|null
     */
    function findPredecessor(Module_Node_Model $node);

    /**
     * @param Module_Node_Model $node
     * @return Module_Node_Model|null
     */
    function findSuccessor(Module_Node_Model $node);

    /**
     * Min and max, can be anything but basically an int, as the ids
     * Return an ordered list of node between the given values (inclusive)
     *
     * @param mixed $min
     * @param mixed $max
     * @return array|Module_Node_Model[]
     */
    function enumerate($min, $max);

    /**
     * Return all Nodes in infixe ordered list.
     *
     * @return mixed
     */
    function infixeList();
}
