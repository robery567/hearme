<?php

/**
 * Class Prototype_DataSource
 */

namespace Architect;
use Silex\Application;

class Prototype_DataSource {
    protected $db;

    /**
     * Prototype_DataSource constructor.
     * @param Application $app
     */
    public function __construct(Application $app) {
        $this->db = $app['db'];
    }
}