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
    protected $Database;

    /**
     * Module_User_Model constructor.
     * @throws Exception
     */
    public function __construct() {
        $this->DataSource = new Module_Database_Model();
        $this->DataSource->setName('hearme_db');
        $this->DataSource->setColumns(['username', 'email', 'password', 'first_name', 'last_name', 'gender', 'avatar', 'online', 'friends']);

        $this->Database = $this->DataSource->load();
    }

    /**
     * Checks if the given user credentials are valid
     * @param $email
     * @param $password
     * @return bool
     * @throws Exception
     */
    public function checkAuthenticationCredentials($email, $password) {
        if (null !== $this->Database->find([$email, $password], null, ['email', 'password'])) {
            return true;
        }

        return false;
    }

    /**
     * Gets the user's data by a given key
     * @param $keyName
     * @param $keyVal
     * @return false|Module_Node_Model
     * @throws Exception
     */
    public function getUserBy($keyName, $keyVal) {
        $userData = $this->Database->find($keyName, null, $keyVal);

        if (null === $userData) {
            return null;
        }

        return $userData->getValue();
    }

    /**
     * Inserts the given user into the database
     * @param $userData
     * @return bool
     */
    public function insertUser($userData) {
        if (!isset($userData['first_name'], $userData['last_name'], $userData['first_name'], $userData['password'], $userData['gender'])) {
            return false;
        }



        return true;
    }
}