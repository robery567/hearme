<?php
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

    public function __construct($app) {
       $this->setUp($app);
    }

    protected function setUp($app) {
        $this->app = $app;
    }

    final public static function getInstance($app) {
        $calledClass = get_called_class();

        if (!isset(self::$instances[$calledClass])) {
            self::$instances[$calledClass] = new $calledClass($app);
        }

        return self::$instances[$calledClass];
    }
}