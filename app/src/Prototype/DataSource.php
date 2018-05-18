<?php

/**
 * Class Prototype_DataSource
 */
class Prototype_DataSource {
    protected $db;

    /**
     * Prototype_DataSource constructor.
     */
    public function __construct() {
        $App = new Silex\Application();
        $this->db = $App['db'];
    }

    public function setUp() {

    }
}