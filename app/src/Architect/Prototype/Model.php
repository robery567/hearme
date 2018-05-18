<?php

/**
 * Class Prototype_Model
 */

namespace Architect;
use Silex\Application;

class Prototype_Model {

    /**
     * @var Application
     */
    protected $app;

    protected $DataSource;

    /**
     * @var array
     */
    protected static $instances = array();

    /**
     * @var self
     */
    protected static $instance;

    /**
     * Prototype_Model constructor.
     * @param Application $app
     */
    public function __construct(Application $app) {
       $this->setUp($app);
    }

    /**
     * Custom constructor
     * @param Application $app
     */
    protected function setUp(Application $app) {
        $this->app = $app;
    }

    /**
     * Get the instance of the class
     * @param Application $app
     * @return mixed
     */
    final public static function getInstance($app) {
        $calledClass = get_called_class();

        if (!isset(self::$instances[$calledClass])) {
            self::$instances[$calledClass] = new $calledClass($app);
        }

        return self::$instances[$calledClass];
    }
}