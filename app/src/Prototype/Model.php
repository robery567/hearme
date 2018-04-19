<?php

/**
 * Class Prototype_Model
 */
class Prototype_Model {

    /**
     * @var Silex\Application
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
     * @param Silex\Application $app
     */
    public function __construct($app) {
       $this->setUp($app);
    }

    /**
     * Custom constructor
     * @param Silex\Application $app
     */
    protected function setUp($app) {
        $this->app = $app;
    }

    /**
     * Get the instance of the class
     * @param Silex\Application $app
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