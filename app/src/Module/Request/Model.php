<?php
/**
 * Created by PhpStorm.
 * User: robery567
 * Date: 17/05/2018
 * Time: 22:52
 */

class Module_Request_Model extends Prototype_Model {
    /** @var Module_User_Model */
    protected $User;

    /**
     * @param Silex\Application $app
     * @param Module_User_Model $User
     * @throws Exception
     */
    public function setUp($app, $User = null) {
        $this->User = (null === $User) ? new Module_Database_Model() : $User;
    }


    /**
     * @param array $request The received request
     * @return array
     * @throws Exception
     */
    public function interpretReceivedRequest($request) {
        if (empty($request['type'])) {
            return [
                'status' => '500',
                'message' => 'INVALID_REQUEST'
            ];
        }

        if ($request['type'] === 'login') {
            if (empty($request['username']) || empty($request['password'])) {
                return [
                    'status' => '500',
                    'message' => 'INVALID_CREDENTIALS'
                ];
            }

            if ($this->User->checkAuthenticationCredentials($request['username'], md5($request['password'])) === false) {
                return [
                    'status' => '500',
                    'message' => 'INVALID_CREDENTIALS'
                ];
            }

            return [
                'status' => '200',
                'message' => 'OK'
            ];
        }

        if ($request['type'] === 'user') {
            if (empty($request['email'])){
                return [
                    'status' => '500',
                    'message' => 'INVALID_REQUEST'
                ];
            }

            $userData = $this->User->getUserBy('email', $request['email']);

            if (null === $userData) {
                return [
                    'status' => '500',
                    'message' => 'NOT_FOUND'
                ];
            }

            unset($userData['password']);

            return [
                'status' => '200',
                'message' => $userData
            ];
        }

        return [
            'status' => '500',
            'message' => 'INVALID_REQUEST'
        ];
    }
}