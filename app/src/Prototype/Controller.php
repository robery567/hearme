<?php
/**
 * Created by PhpStorm.
 * User: robery567
 * Date: 18/05/2018
 * Time: 12:06
 */

namespace Architect;

use Silex\Application;
use Symfony\Component\HttpFoundation\Response;

abstract class Prototype_Controller {
    public $app;

    public function __construct(Application $app) {
        $this->app = $app;
    }

    public function getParam($key) {
        $postParams = $this->app['request_stack']->getCurrentRequest()->request->all();
        $getParams = $this->app['request_stack']->getCurrentRequest()->query->all();

        if (isset($postParams[$key])) {
            return $postParams[$key];
        } elseif (isset($getParams[$key])) {
            return $getParams[$key];
        } else {
            return null;
        }
    }

    public function render($view, array $parameters = array()) {
        $response = new Response();
        return $response->setContent($this->app['twig']->render($view, $parameters));
    }

}