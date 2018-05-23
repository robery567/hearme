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

    /**
     * Module_User_Model constructor.
     * @throws Exception
     */
    public function __construct() {
        $this->DataSource = new Module_Database_Model();
        $this->DataSource->setName('hearme_db');
        $this->DataSource->setColumns(['username', 'email', 'password', 'first_name', 'last_name', 'gender', 'avatar', 'online', 'friends']);
        $this->DataSource = $this->DataSource->load();
    }

    /**
     *
     * @param $email
     * @param $password
     * @return bool
     * @throws Exception
     */
    public function checkAuthenticationCredentials($email, $password) {
        if (null !== $this->DataSource->find([$email, $password], null, ['email', 'password'])) {
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
        $userData = $this->DataSource->find($keyName, null, $keyVal);

        if (null === $userData) {
            return null;
        }

        return $userData->getValue();
    }
}