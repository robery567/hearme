<?php

/**
 * Class Prototype_DataSource
 */
class Prototype_DataSource {
    protected $db;

    /**
     * Prototype_DataSource constructor.
     * @param Silex\Application $app
     */
    public function __construct($app) {
        $this->setUp($app);
    }

    /**
     * @param Silex\Application $app
     */
    public function setUp($app) {
        $this->db = $app['db'];
    }

}