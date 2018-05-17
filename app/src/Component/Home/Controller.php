<?php
/**
 * Created by PhpStorm.
 * User: robery567
 * Date: 17/05/2018
 * Time: 11:36
 */

class Component_Home_Controller {
    public function indexAction() {
        return json_encode(
            [
                'status' => '300',
                'message' => 'allgood'
            ]
        );
    }
}