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
    protected $databasePath = __DIR__ . '/app/db/';

    /**
     * @var Module_Tree_Model|null The database data Tree
     */
    protected $databaseData = null;

    /**
     * @var string The database file extension
     */
    protected $databaseExtension = 'json';

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
        $fileToLoad = realpath($this->databasePath . $this->databaseName . '.' . $this->databaseExtension);
        die($fileToLoad);

        $Tree = new Module_Tree_Model();

        // if the database file doesn't exist, we create a blank one
        if (!file_exists($fileToLoad)) {
            file_put_contents($fileToLoad, json_encode([]));

            return $Tree;
        }

        $databaseData = file_get_contents($fileToLoad);

        if (($databaseData = json_decode($databaseData, true)) === null) {
            throw new Exception('The database file is corrupted');
        }

        /**
         * @TODO: Dinamically set the user columns to be loaded (Too hurried now...)
         */
        foreach ($databaseData as $user) {
            $Node = new Module_Node_Model($user['id'], [
                'username' => $user['username'],
                'email' => $user['email'],
                'gender' => $user['gender']
            ]);

            $Tree->insert($Node);
        }

        return $Tree;
    }
}