<?php
/**
 * Created by PhpStorm.
 * User: robery567
 * Date: 17/05/2018
 * Time: 12:20
 */
class Component_Request_Controller {
    public function indexAction() {
        return json_encode(
            [
                'status' => '200',
                'message' => 'allgood'
            ]
        );
    }
}