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
        $this->User = (null === $User) ? new Module_User_Model() : $User;
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
            if (empty($request['email']) || empty($request['password'])) {
                return [
                    'status' => '500',
                    'message' => 'EMPTY_FIELDS'
                ];
            }

            if ($this->User->checkAuthenticationCredentials($request['email'], md5($request['password'])) === false) {
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
                'message' => json_encode($userData)
            ];
        }

        if ($request['type'] === 'register') {
            if (!isset($request['email'], $request['first_name'], $request['last_name'], $request['first_name'], $request['password'], $request['gender'])) {
                return [
                    'status' => '500',
                    'message' => 'INVALID_REQUEST'
                ];
            }

            $insertResponse = $this->User->insertUser($request);
            if ($insertResponse === false) {
                return [
                    'status' => '500',
                    'message' => 'EXIST'
                ];
            }

            if ($insertResponse === -1) {
                return [
                    'status' => '500',
                    'message' => 'EMPTY_FIELDS'
                ];
            }

            return [
                'status' => '200',
                'message' => 'OK'
            ];
        }

        if ($request['type'] === 'addfriend') {
            if (!isset($request['origin_email'], $request['friend_email'])) {
                return [
                    'status' => '500',
                    'message' => 'INVALID_REQUEST'
                ];
            }

            $addFriendResponse = $this->User->addFriend($request['origin_email'], $request['friend_email']);

            if ($addFriendResponse === false) {
                return [
                    'status' => '500',
                    'message' => 'ADD_ERROR'
                ];
            }

            if ($addFriendResponse === -1) {
                return [
                    'status' => '500',
                    'message' => 'ALREADY_FRIEND'
                ];
            }

            return [
                'status' => '200',
                'message' => 'OK'
            ];
        }


        if ($request['type'] === 'searchfriend') {
            if (!isset($request['origin_email'], $request['friend_email'])) {
                return [
                    'status' => '500',
                    'message' => 'INVALID_REQUEST'
                ];
            }

            $searchFriendsResponse = $this->User->searchFriend($request['origin_email'], $request['friend_email']);

            return [
                'status' => '200',
                'message' => json_encode($searchFriendsResponse)
            ];
        }

        if ($request['type'] === 'upload_avatar') {
            if (empty($_FILES['file']) || empty($request['user'])) {
                return [
                    'status' => '500',
                    'message' => 'INVALID_REQUEST'
                ];
            }

            $soundsPath = $_SERVER['DOCUMENT_ROOT'] . '/../public/sounds';

            if (is_uploaded_file($_FILES['file']['tmp_name']) === false) {
                return [
                    'status' => '500',
                    'message' => 'ERROR_UPLOADING'
                ];
            }

            $avatarFile = $soundsPath . basename($_FILES['file']['name']);

            if (move_uploaded_file($_FILES['file']['tmp_name'], $avatarFile) === false){
                return [
                    'status' => '500',
                    'message' => 'INVALID_FILE'
                ];
            }

            $avatarUrl = 'http://sandbox.robertcolca.me/sounds/' . basename($_FILES['file']['name']);

            if ($this->User->updateAvatar($request['origin_email'], $avatarUrl) === false) {
                return [
                    'status' => '500',
                    'message' => 'ERROR_UPDATING'
                ];
            }

            return [
                'status' => '200',
                'message' => 'OK'
            ];
        }

        return [
            'status' => '500',
            'message' => 'INVALID_REQUEST'
        ];
    }
}
