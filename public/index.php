<?php
/*
*
* @Name : Architectus Framework
* @Author: Robert Mihai Colca
* @Version: 0.5 (DEV)
*
*/
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\Response;

date_default_timezone_set('Europe/Bucharest');

require_once __DIR__ . '/../vendor/autoload.php';
require_once __DIR__ . '/../app/settings/database.php';
require_once __DIR__ . '/../app/autoload.php';

error_reporting(E_ALL);
ini_set('display_errors', 'On');

$app = new Silex\Application();
$app['debug'] = true;

$app->register(new Silex\Provider\DoctrineServiceProvider(), $connection);
$app->register(new Silex\Provider\SessionServiceProvider());

// Cache
$app->register(new Silex\Provider\TwigServiceProvider(), array(
    'twig.path' => __DIR__ . '/../app/views',
    'twig.options' => array(
        'cache' => __DIR__ . '/../cache',
    ),
));

$app->register(new Silex\Provider\HttpCacheServiceProvider(), array(
    'http_cache.cache_dir' => __DIR__ . '/../cache',
));

$app->extend('twig', function ($twig) {
    /**
     * @var Twig_ExtensionSet $twig
     */
    $twig->addFunction(new \Twig_SimpleFunction('asset', function ($asset) {
        return sprintf('http://' . $_SERVER['HTTP_HOST'] . '/assets/%s', ltrim($asset, '/'));
    }));

    $twig->addExtension(new Twig_Extensions_Extension_Text());

    return $twig;
});

$lang = json_decode(file_get_contents(__DIR__ . '/../app/lang/ro.json'), true);
$app['twig']->addGlobal('text', $lang);

$urlMethodCall = (!empty($_POST)) ? 'post' : 'get';

Component_Settings_Model::getInstance($app)->instantiatePage($urlMethodCall);


$app->error(function (\Exception $e, $code) use ($app) {
    $response = [
        'status' => '500',
        'response' => json_encode(['message' => 'Ooops... perhaps you\'ve got into the wrong place, at the wrong moment :('])
    ];

    if (!empty($_GET['exception'])) {
        $response['message'] = json_encode('The following exception has been thrown: ' . htmlspecialchars($_GET['exception'], HTML_ENTITIES));
    }

    return new Response(json_encode($response));
});

$app->run();
