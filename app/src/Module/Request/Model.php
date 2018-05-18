<?php
/**
 * Created by PhpStorm.
 * User: robery567
 * Date: 17/05/2018
 * Time: 22:52
 */

namespace Architect;
use Silex\Application;

class Module_Request_Model extends Prototype_Model {
    /** @var Module_Request_DataSource */
    protected $DataSource;

    /**
     * @param Application $app
     */
    public function setUp(Application $app)
    {
        $this->DataSource = new Module_Request_DataSource($app);
    }


    /**
     * @param array $request The received request
     */
    public function interpretReceivedRequest($request) {

    }
}