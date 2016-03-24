 function RegisterGCM() {
        //register device to GCM & send key to server 
        var push = PushNotification.init({
            "android": { "senderID": "268098525942" },
            "ios": { "alert": "true", "badge": "true", "sound": "true" },
            "windows": {}
        });

        push.on('registration', function (data) {
            //send deviceid to application server 
            // console.log(JSON.stringify(data));
            url = WebUrlService.url + "sendNotification/" + data.registrationId;
            $scope.Login_Information = url;
            $http.get(url).then(function (response) {
                // $scope.Info = response.data.sendNotificationResult;
            });
        });

        push.on('notification', function (data) {
            $scope.Info = data.message;
            // data.message,
            // data.title,
            // data.count,
            // data.sound,
            // data.image,
            // data.additionalData
        });

        push.on('error', function (e) {
            console.log(data.message);
        });
    }