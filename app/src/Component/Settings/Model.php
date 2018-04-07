<?php
class Component_Settings_Model extends Prototype_Model {
    /**
     * @var Component_Settings_DataSource
     */
    protected $DataSource;

    /**
     * @var Silex\Application
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
    protected function setUp($app) {
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

    public function getUrlParts() {
        if (empty($_SERVER['PATH_INFO'])) {
            return null;
        }

        return explode('/', $_SERVER['PATH_INFO']);
    }

    public function getUrlAllParts() {
        if (empty($_SERVER['PATH_INFO'])) {
            return null;
        }

        return $_SERVER['PATH_INFO'];
    }

    public function instantiatePage($method) {
        $app = $this->app;

        $this->app->$method($this->getUrlAllParts(), function () use ($app) {
            $urlParts = $this->getUrlParts();

            $partsCount = count($urlParts);
            $component =  ($partsCount > 1) ? $urlParts[1] : $this->defaultComponent;
            $action =  ($partsCount > 2) ? $urlParts[2] : $this->defaultAction;

            try {
                $response = $this->app['twig']->render($component . '/' . $action . $this->twigExtension, array(
                    'settings' => $this->getAllSettings(),
                ));
            } catch (\Exception $e) {
                return $this->app->redirect('/home');
            }

            return $response;
        });
    }
}