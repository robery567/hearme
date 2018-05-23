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

        $receivedRequest = file_get_contents("php://input");

        if (!empty($receivedRequest) || !empty($receivedRequest)) {
            $decodedRequest = [];

            if (!empty($receivedRequest)) {
                $decodedRequest = json_decode(urldecode($receivedRequest), true);
            }

            $interpretedRequest = $Request->interpretReceivedRequest($decodedRequest);

            return json_encode(
                [
                    'status' => ($interpretedRequest['status'] !== '200') ? '500' : '200',
                    'response' => json_encode(is_array($interpretedRequest['message']) ? $interpretedRequest : ['message' => $interpretedRequest['message']])
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
