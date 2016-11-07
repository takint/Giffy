var Giffy = angular.module('Giffy', [
    'ngAnimate',
    'ngResource',
    'ui.router',
    'ui.bootstrap',
    'angular-loading-bar',
    'oc.lazyLoad',
    'nouislider',
    'ngTable',
    'LocalStorageModule',
    'angularHideHeader',
    'validation',
    'validation.rule'
])

var baseUrl = $('base').attr('href');
var appBaseUrl = baseUrl + 'wwwroot/app/';
var hostName = baseUrl;

Giffy.constant('ngAuthSettings', {
    apiServiceBaseUri: baseUrl,
    clientId: 'Web.BongVL'
});

Giffy.run(['authService', '$window', function (authService, $window) {
    authService.fillAuthData();

    $window.fbAsyncInit = function () {
        FB.init({
            appId: GiffyData.FacebookAppId,
            xfbml: true,
            version: 'v2.5'
        });

        // Get Embedded Video Player API Instance
        var my_video_player;
        FB.Event.subscribe('xfbml.ready', function (msg) {
            if (msg.type === 'video') {
                my_video_player = msg.instance;
            }
        });
    };

    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) { return; }
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/en_US/sdk.js";
        fjs.parentNode.insertBefore(js, fjs);
    }(document, 'script', 'facebook-jssdk'));
}]);