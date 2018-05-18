<?php

/**
 * Class Component_Settings_Model
 */

namespace Architect;
use Silex\Application;

class Component_Settings_Model extends Prototype_Model {
    /**
     * @var Component_Settings_DataSource
     */
    protected $DataSource;

    /**
     * @var Application
     */
    protected $app;
    protected $twigExtension = '.twig';
    protected $defaultComponent = 'home';
    protected $defaultAction = 'index';

    /**
     * Settings array
     * @var array
     */
    private $settings = [
        'site_name' => "CYF",
        'version' => '1.00',
        'language' => 'en',
    ];

    /**
     * @param $app
     */
    protected function setUp(Application $app) {
        $this->DataSource = new Component_Settings_DataSource($app);
        $this->app = $app;
    }

    /**
     * Returns the site name
     * @return string
     */
    public function getSiteName() {
        return $this->settings['site_name'];
    }

    /**
     * Return the site version
     * @return string
     */
    public function getSiteVersion() {
        return $this->settings['version'];
    }

    /**
     * Return the site language
     * @return string
     */
    public function getSiteLanguage() {
        return $this->settings['language'];
    }

    /**
     * Returns an array with all the site settings
     * @return array
     */
    public function getAllSettings() {
        return $this->settings;
    }

    /**
     * @return false|string
     */
    public function getYear() {
        return date("Y");
    }

    /**
     * @return array|null
     */
    public function getUrlParts() {
        if (empty($_SERVER['REQUEST_URI'])) {
            return [];
        }

        $returnUrlParts = $_SERVER['REQUEST_URI'];

        return explode('/', $returnUrlParts);
    }

    /**
     * @return null|string
     */
    public function getUrlAllParts() {
        if (empty($_SERVER['REQUEST_URI'])) {
            return null;
        }

        return $_SERVER['REQUEST_URI'];
    }

    /**
     * Instantiates the page with the specific method
     * @param string $method GET/POST
     */
    public function instantiatePage($method) {
        $app = $this->app;

        $this->app->$method($this->getUrlAllParts(), function () use ($app) {
            $urlParts = $this->getUrlParts();

            $partsCount = count($urlParts);
            $component =  ($partsCount > 1) ? $urlParts[1] : $this->defaultComponent;
            $action =  ($partsCount > 2) ? $urlParts[2] : $this->defaultAction;

            try {
                $expectedControllerName = 'Component_' . ucfirst($component) . '_Controller';

                if (!class_exists($expectedControllerName)) {
                    $response = $this->app['twig']->render($this->defaultAction . $this->twigExtension, array(
                        'settings' => $this->getAllSettings(),
                    ));
                } else {
                    $Controller = new $expectedControllerName();

                    $expectedActionName = strtolower($action) . 'Action';

                    if (!method_exists($Controller, $expectedActionName)) {
                        $Controller->$expectedActionName();
                    } else {
                        $defaultActionName = $this->defaultAction . 'Action';

                        if (!method_exists($expectedControllerName, $this->defaultAction . 'Action')) {
                            throw new \Exception("No index action defined in controller");
                        }

                        $response = $Controller->$defaultActionName($app);
                    }
                }

                if (empty($response)) {
                    throw new \Exception("No response returned from controller");
                }
            } catch (\Exception $e) {
                return $this->app->redirect('/home?' . http_build_query(['exception' => $e->getMessage()]));
            }

            return $response;
        });
    }
}