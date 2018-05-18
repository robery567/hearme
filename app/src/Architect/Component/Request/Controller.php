<?php
/**
 * Created by PhpStorm.
 * User: robery567
 * Date: 17/05/2018
 * Time: 12:20
 */

namespace Architect;

class Component_Request_Controller extends Prototype_Controller {
    public function indexAction() {
        $Request = new Module_Request_Model($this->app);

        if (!empty($_POST['request'])) {
            $decodedRequest = json_decode($_POST['request'], true);

            $Request->interpretReceivedRequest($decodedRequest);
        }

        return json_encode(
            [
                'status' => '200',
                'response' => json_encode(['message' => 'allgood'])
            ]
        );
    }
}