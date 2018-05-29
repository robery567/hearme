<?php

/**
 * Class Module_Tree_Model
 */
class Module_Tree_Model extends Module_Tree_Abstract_Model {
    /**
     * @param int $idA
     * @param int $idB
     * @return bool
     */
    protected function compare($idA, $idB) {
        if ($idA === $idB) {
            return 0;
        }

        return $idA < $idB ? 1 : -1;
    }
}
