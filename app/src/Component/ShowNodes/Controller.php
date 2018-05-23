<?php
/**
 * Created by PhpStorm.
 * User: robery567
 * Date: 22/05/2018
 * Time: 19:54
 */

/**
 * Class Component_ShowNodes_Controller
 */
class Component_ShowNodes_Controller extends Prototype_Controller {
    /**
     * @throws Exception
     */
    public function indexAction() {
        $Database = new Module_Database_Model();
        $Database->setName('hearme');
        $Database->setColumns(['username', 'email', 'gender']);
        //$Database->load();

        //$Tree = $Database->getDatabaseData();

        $User = new Module_User_Model();

        var_dump($User->checkAuthenticationCredentials('robery_office@yahoo.ro', 'aaaa'));

        $Printer = new Module_Tree_Printer_Model();

        //return $Printer->render($Tree);
    }
}