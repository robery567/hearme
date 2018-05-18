<?php
/**
 * Created by PhpStorm.
 * User: robery567
 * Date: 18/05/2018
 * Time: 12:09
 */

namespace Architect;

use Silex\ControllerResolver as BaseControllerResolver;

class Base_ControllerResolver extends BaseControllerResolver {
    protected function instantiateController($class) {
        return new $class($this->app);
    }
}