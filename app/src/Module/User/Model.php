<?php
/**
 * Created by PhpStorm.
 * User: robery567
 * Date: 23/05/2018
 * Time: 17:31
 */

class Module_User_Model {

    /** @var Module_Database_Model */
    protected $DataSource;

    /** @var Module_Tree_Model */
    protected $Tree;

    /**
     * Module_User_Model constructor.
     * @throws Exception
     */
    public function __construct() {
        $this->DataSource = new Module_Database_Model();
        $this->DataSource->setName('hearme_db');
        $this->DataSource->setColumns(['username', 'email', 'password', 'first_name', 'last_name', 'gender', 'avatar', 'online', 'friends', 'messages']);

        $this->Tree = $this->DataSource->load();
    }

    /**
     * Checks if the given user credentials are valid
     * @param string $email
     * @param string $password
     * @return bool
     * @throws Exception
     */
    public function checkAuthenticationCredentials($email, $password) {
        return null !== $this->Tree->find([$email, $password], null, ['email', 'password']);
    }

    /**
     * Gets the user's data by a given key
     * @param string $keyName
     * @param string $keyVal
     * @return false|Module_Node_Model
     * @throws Exception
     */
    public function getUserBy($keyName, $keyVal) {
        $userData = $this->Tree->find($keyVal, null, $keyName);

        if (null === $userData) {
            return null;
        }

        return $userData->getValue();
    }

    /**
     * Inserts the given user into the database
     * @param array $userData
     * @return bool
     * @throws Exception
     */
    public function insertUser($userData) {
        if (!isset($userData['email'], $userData['first_name'], $userData['last_name'], $userData['first_name'], $userData['password'], $userData['gender'])) {
            return -1;
        }

        if (null !== $this->Tree->find($userData['email'], null, 'email')) {
            return false;
        }

        $userData['password'] = md5($userData['password']);
        $userData['friends'] = ['0'];
        $userData['avatar'] = 'http://sandbox.robertcolca.me/sounds/noavatar.mp3';

        return $this->DataSource->insert($userData);
    }

    /**
     * Add an email to the friends list of the user
     * @param string $originEmail
     * @param string $friendEmail
     * @return bool
     * @throws Exception
     */
    public function addFriend($originEmail, $friendEmail) {
        $friendData = $this->Tree->find($friendEmail, null, 'email');

        if (null === $friendData) {
            return false;
        }

        $foundUser = $this->Tree->find($originEmail, null, 'email');
        $userData = $foundUser->getValue();

        if (empty($userData)) {
            return false;
        }

        foreach ($userData['friends'] as $friend) {
            if ($friend === $friendEmail) {
                return -1;
            }
        }

        $userData['id'] = $foundUser->getId();

        if (empty($userData['friends'][0])) {
            $userData['friends'][0] = $friendEmail;
        } else {
            $userData['friends'][] = $friendEmail;
        }

        return $this->DataSource->update($userData);
    }

    /**
     * Adds a message from an email to the destination email into the database
     * @param string $destinationEmail
     * @param string $friendEmail
     * @param string $message
     * @return bool
     * @throws Exception
     */
    public function addMessage($destinationEmail, $friendEmail, $message) {
        $friendData = $this->Tree->find($friendEmail, null, 'email');

        if (null === $friendData) {
            return false;
        }

        $foundUser = $this->Tree->find($destinationEmail, null, 'email');
        $userData = $foundUser->getValue();

        if (empty($userData)) {
            return false;
        }

        $userData['id'] = $foundUser->getId();

        if (empty($userData['messages'][0])) {
            $userData['messages'][0] = [$friendEmail => $message];
        } else {
            $userData['messages'][] = [$friendEmail => $message];
        }

        return $this->DataSource->update($userData);
    }

    /**
     * Updates user avatar
     * @param string $originEmail
     * @param string $avatarUrl
     * @return bool
     * @throws Exception
     */
    public function updateAvatar($originEmail, $avatarUrl) {
        $userData = $this->Tree->find($originEmail, null, 'email');

        if (null === $userData) {
            return false;
        }

        $userId = $userData->getId();
        $userData = $userData->getValue();
        $userData['id'] = $userId;
        $userData['avatar'] = $avatarUrl;

        return $this->DataSource->update($userData);
    }

    /**
     * Search for a friend in the list of someone's friends (Partial/exact search)
     * @param string $originEmail
     * @param string $friendEmail
     * @param string $searchType partial/exact
     * @return bool|array
     * @throws Exception
     */
    public function searchFriend($originEmail, $friendEmail, $searchType = 'partial') {
        $foundUser = $this->Tree->find($originEmail, null, 'email');
        $userData = $foundUser->getValue();

        if (empty($userData)) {
            return [];
        }

        $friendsToReturn = [];

        foreach ($userData['friends'] as $friend) {
            if ($searchType === 'exact' && $friend === $friendEmail) {
                return $friendEmail;
            }

            if ($searchType === 'partial' && !empty(strstr($friend, $friendEmail))) {
                $friendsToReturn[] = $friend;
            }
        }

        return $friendsToReturn;
    }
}