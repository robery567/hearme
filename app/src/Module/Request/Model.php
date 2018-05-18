<?php
/**
 * Created by PhpStorm.
 * User: robery567
 * Date: 17/05/2018
 * Time: 22:52
 */

class Module_Request_Model {
    /** @var Module_Request_DataSource */
    protected $DataSource;

    /**
     * @param Module_Request_DataSource $DataSource
     */
    public function setUp($DataSource = null) {
        $this->DataSource = (null === $DataSource) ? new Module_Request_DataSource() : $DataSource;
    }


    /**
     * @param array $request The received request
     */
    public function interpretReceivedRequest($request) {

    }
}