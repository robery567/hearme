<?php
/**
 * Created by PhpStorm.
 * User: robery567
 * Date: 17/05/2018
 * Time: 12:20
 */
class Component_Request_Controller extends Prototype_Controller {
    /**
     * @return string
     * @throws Exception
     */
    public function indexAction() {
        $Request = new Module_Request_Model($this->app);

        if (!empty($_POST['request']) || !empty($_GET['request'])) {
            $decodedRequest = [];
            if (!empty($_POST['request'])) {
                $decodedRequest = json_decode($_POST['request'], true);
            }

            if (!empty($_GET['request'])) {
                $decodedRequest = json_decode($_GET['request'], true);
            }

            $interpretedRequest = $Request->interpretReceivedRequest($decodedRequest);

            return json_encode(
                [
                    'status' => ($interpretedRequest['status'] !== '200') ? '500' : '200',
                    'response' => json_encode(['message' => $interpretedRequest['message']])
                ]
            );
        }

        return json_encode(
            [
                'status' => '200',
                'response' => json_encode(['message' => 'allgood'])
            ]
        );
    }
}