<?php
/**
 * Created by PhpStorm.
 * User: robery567
 * Date: 18/05/2018
 * Time: 13:36
 */

class Prototype_Controller {
    /**
     * @var Silex\Application
     */
    protected $app;

    /**
     * Prototype_Controller constructor.
     * @param $app
     */
    public function __construct($app) {
        $this->app = $app;
    }
}