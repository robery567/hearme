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
     */
    public function checkAuthenticationCredentials($email, $password) {
        if (null !== $this->DataSource->find($email, null, 'email')) {
            return true;
        }

        return false;
    }
}