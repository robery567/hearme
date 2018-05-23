<?php

/**
 * Class Model_Node_Model
 */
class Module_Node_Model extends Module_Node_Abstract_Model {
    /**
     * @return string
     */
    public function __toString() {
        return (string)$this->getValue();
    }

    /**
     * Parent of the node
     *
     * @var Module_Node_Model
     */
    protected $parent = null;

    /**
     * @return int
     */
    public function getId() {
        return $this->id;
    }

    /**
     * @param int $id
     * @return $this
     */
    public function setId($id) {
        $this->id = $id;

        return $this;
    }

    /**
     * @return mixed
     */
    public function getValue() {
        return $this->value;
    }

    /**
     * @param mixed $value
     * @return $this
     */
    public function setValue($value) {
        $this->value = $value;
        return $this;
    }
}
