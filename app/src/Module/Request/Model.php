<?php
/**
 * Created by PhpStorm.
 * User: robery567
 * Date: 17/05/2018
 * Time: 22:52
 */

class Module_Request_Model extends Prototype_Model {
    /** @var Module_Database_Model */
    protected $DataSource;

    /**
     * @param Silex\Application $app
     * @param Module_Database_Model $DataSource
     * @throws Exception
     */
    public function setUp($app, $DataSource = null) {
        $this->DataSource = new Module_Database_Model();
        $this->DataSource->setName('hearme_db');
        $this->DataSource->setColumns(['username', 'email', 'password']);
        $this->DataSource->load();
    }


    /**
     * @param array $request The received request
     */
    public function interpretReceivedRequest($request) {

    }
}