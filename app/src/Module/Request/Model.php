<?php
/**
 * Created by PhpStorm.
 * User: robery567
 * Date: 17/05/2018
 * Time: 22:52
 */

class Module_Request_Model extends Prototype_Model {
    /** @var Module_Request_DataSource */
    protected $DataSource;

    /**
     * @param Silex\Application $app
     * @param Module_Request_DataSource $DataSource
     */
    public function setUp($app, $DataSource = null) {
        $this->DataSource = (null === $DataSource) ? new Module_Request_DataSource($app) : $DataSource;
    }


    /**
     * @param array $request The received request
     */
    public function interpretReceivedRequest($request) {

    }
}