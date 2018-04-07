<?php

class Test extends \PHPUnit_Framework_TestCase
{
    public function createApplication()
    {
        return require __DIR__.'/../app.php';
    }
}
