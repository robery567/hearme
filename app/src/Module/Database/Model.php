<?php
/**
 * Created by PhpStorm.
 * User: robery567
 * Date: 23/05/2018
 * Time: 13:12
 */

/**
 * Class Module_Database_Model
 */
class Module_Database_Model {
    /** @var string The database name to be loaded */
    protected $databaseName;

    /**
     * @var string The database path
     */
    protected $databasePath = '/../app/db/';

    /**
     * @var Module_Tree_Model|null The database data Tree
     */
    protected $databaseData = null;

    /**
     * @var string The database file extension
     */
    protected $databaseExtension = 'json';

    /**
     * @var array The database columns
     */
    protected $databaseColumns = ['id'];

    /**
     * Sets the database name to be loaded
     * @param string $name
     * @throws Exception
     */
    public function setName($name) {
        if (empty($name)) {
            throw new Exception('No database name has been specified');
        }

        $this->databaseName = $name;
    }

    /**
     * Set the columns names
     * @param $columns
     * @throws Exception
     */
    public function setColumns($columns) {
        if (empty($columns)) {
            throw new Exception('No column specified');
        }

        if (!is_array($columns)) {
            throw new Exception('Invalid columns format');
        }

        $this->databaseColumns[] = $columns;
    }

    /**
     * Sets the database name to be loaded
     * @param string $extension
     * @throws Exception
     */
    public function setExtension($extension) {
        if (empty($extension)) {
            throw new Exception('No extension has been specified');
        }

        $this->databaseExtension = $extension;
    }

    /**
     * Get the database data
     * @return Module_Tree_Model|null
     */
    public function getDatabaseData() {
        if (empty($this->databaseData)) {
            return new Module_Tree_Model();
        }

        return $this->databaseData;
    }

    /**
     * Loads the database into the Tree
     * @return Module_Tree_Model
     * @throws Exception
     */
    public function load() {
        $fileToLoad = $_SERVER['DOCUMENT_ROOT'] . $this->databasePath . $this->databaseName . '.' . $this->databaseExtension;

        $Tree = new Module_Tree_Model();

        // if the database file doesn't exist, we create a blank one
        if (!file_exists($fileToLoad)) {
            file_put_contents($fileToLoad, json_encode([]));

            $this->databaseData = $Tree;

            return $Tree;
        }

        $databaseData = file_get_contents($fileToLoad);

        if (($databaseData = json_decode($databaseData, true)) === null) {
            throw new Exception('The database file is corrupted');
        }

        foreach ($databaseData as $user) {
            $userData = $this->generateUserData($user);

            $Node = new Module_Node_Model($user['id'], $userData);

            $Tree->insert($Node);
        }

        $this->databaseData = $Tree;

        return $Tree;
    }

    /**
     * Gets the user data array
     * @param array $user
     * @return array
     * @throws Exception
     */
    private function generateUserData($user) {
        $userData = [];

        foreach ($this->databaseColumns as $column) {
            if ($column === 'id') {
                continue;
            }

            if (empty($user[(string)$column])) {
                throw new Exception('The database columns are corrupted');
            }

            $userData[] = [
                $column => $user[$column]
            ];
        }

        return $userData;
    }
}