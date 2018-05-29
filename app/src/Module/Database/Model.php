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
    protected $databaseData;

    /**
     * @var string The database file extension
     */
    protected $databaseExtension = 'json';

    /**
     * @var array The database columns
     */
    protected $databaseColumns = ['id'];

    /**
     * @var string The full path of the database to load
     */
    protected $fileToLoad = '';

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

        foreach ($columns as $column) {
            $this->databaseColumns[] = $column;
        }
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
     * @param $fileToLoad
     */
    public function setDatabaseFileToLoad($fileToLoad) {
        $this->fileToLoad = $fileToLoad;
    }

    /**
     * Get the database data
     * @return Module_Tree_Model|null
     */
    public function getDatabaseData() {
        if (null === $this->databaseData) {
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
        $this->setDatabaseFileToLoad($_SERVER['DOCUMENT_ROOT'] . $this->databasePath . $this->databaseName . '.' . $this->databaseExtension);

        $Tree = new Module_Tree_Model();

        $databaseData = $this->readDatabase();

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
     * Insert given data into the database
     * @param $data
     * @return bool
     * @throws Exception
     */
    public function insert($data) {
        if (!is_array($data)) {
            throw new Exception('Invalid data to insert into the database');
        }

        if ($this->validateColumns(array_keys($data)) === false) {
            return false;
        }

        $dataToInsert = [];

        foreach ($this->databaseColumns as $column) {
            if ($column === 'id') {
                $dataToInsert['id'] = $this->getLastId() + 1;
                continue;
            }

            if (empty($data[$column])) {
                $dataToInsert[$column] = 'null';
                continue;
            }

            $dataToInsert[$column] = $data[$column];
        }

        $dbData = json_decode($this->readDatabase(), true);
        $dbData[] = $dataToInsert;

        $this->writeToDatabase(stripslashes(json_encode($dbData)));

        return true;
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

            $userData[$column] = $user[$column];
        }

        return $userData;
    }

    /**
     * @param $entryData
     * @return bool
     * @throws Exception
     */
    public function update($entryData) {
        if (empty($entryData['id'])) {
            return false;
        }

        $databaseData = json_decode($this->readDatabase());
        $userKey = $this->getEntryPosition($entryData['id']);

        if (empty($databaseData[$userKey])) {
            return false;
        }

        $databaseData[$userKey] = $entryData;

        $databaseData = json_encode($databaseData);

        $this->writeToDatabase(stripslashes($databaseData));

        return true;
    }

    /**
     * Reads the database file
     * @return bool|string
     */
    public function readDatabase() {
        // if the database file doesn't exist, we create a blank one
        if (!file_exists($this->fileToLoad)) {
            $this->writeToDatabase(json_encode([]));
        }

        return file_get_contents($this->fileToLoad);
    }

    /**
     * Write to database file
     * @param $data
     */
    private function writeToDatabase($data) {
        file_put_contents($this->fileToLoad, $data);
    }

    /**
     * Validate if the set of columns are valid
     * @param $columns
     * @return bool
     */
    private function validateColumns($columns) {
        foreach ($columns as $column) {
            if ($column === 'type') {
                continue;
            }

            if (!in_array($column, $this->databaseColumns, true)) {
                return false;
            }
        }

        return true;
    }

    /**
     * Gets the database key of a user
     * @param $id
     * @return int|string
     * @throws Exception
     */
    private function getEntryPosition($id) {
        $databaseData = json_decode($this->readDatabase());

        foreach ($databaseData as $key => $user) {
            if ($user->id === $id) {
                return $key;
            }
        }

        return $this->getLastId()+1;
    }

    /**
     * Gets the last entry id
     * @throws Exception
     * return int
     */
    private function getLastId() {
        $databaseData = $this->readDatabase();

        if (($databaseData = json_decode($databaseData, true)) === null) {
            throw new Exception('The database file is corrupted');
        }

        $lastEntry = end($databaseData);

        return $lastEntry['id'];
    }
}