<?php

class Prototype_DataSource {
    protected $db;

    public function __construct($app) {
        $this->setUp($app);
    }

    public function setUp($app) {
        $this->db = $app['db'];
    }

}