<?php
/**
 * Created by PhpStorm.
 * User: Robery
 * Date: 11/18/2016
 * Time: 11:13 PM
 */
function autoload($className) {
    $className = ltrim($className, '\\');
    $fileName = '';
    $namespace = '';
    if ($lastNsPos = strrpos($className, '\\')) {
        $namespace = substr($className, 0, $lastNsPos);
        $className = substr($className, $lastNsPos + 1);
        $fileName = str_replace('\\', DIRECTORY_SEPARATOR, $namespace) . DIRECTORY_SEPARATOR;
    }
    $fileName .= str_replace('_', DIRECTORY_SEPARATOR, $className) . '.php';

    if (strpos(strtolower($fileName), 'symfony') === false) {
        require 'src/'.$fileName;
    }
}

spl_autoload_register('autoload');