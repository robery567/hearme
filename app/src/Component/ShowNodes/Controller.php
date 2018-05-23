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
        $Database->load();

        $Tree = $Database->getDatabaseData();

        $Printer = new Module_Tree_Printer_Model();

        return $Printer->render($Tree);
    }
}