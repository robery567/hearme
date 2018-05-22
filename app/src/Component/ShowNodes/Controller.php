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
        $nodes = [
            10, 85, 15,
            70, 20, 60, 30,
            50, 65, 80, 90,
            40, 5, 55
        ];

        $Tree = new Module_Tree_Model();

        foreach ($nodes as $id) {
            $Node = new Module_Node_Model($id, $id);
            $Tree->insert($Node);
        }

        $removeNode = [
            30,
            70,
            60,
            15,
        ];

        foreach ($removeNode as $id) {
            $Tree->remove($Tree->find($id));
        }

        $Printer = new Module_Tree_Printer_Model();

        return $Printer->render($Tree);
    }
}