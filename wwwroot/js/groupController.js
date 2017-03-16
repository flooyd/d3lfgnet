(function () {
  var app = angular.module('app');
  app.controller('groupController', groupController);
  app.controller('chatController', chatController);

  function chatController($scope) {
    $scope.showChat = false;
    $scope.hello = function () {
      $scope.showChat = true;
    }
  }


  function groupController($http, $scope, $timeout) {
    var vm = this;
    vm.groups = [];
    vm.showDifficulty = true;
    vm.greaterRift = false;
    vm.battletag = "a";
    vm.groupCreated = false;
    vm.newGroup = {
      activity: 'Rifts',
      paragon: '0',
      difficulty: 'Torment XI',
      description: '',
      grLevel: '',
      region: 'North America',
      battleTag: 'Rahl#1739'
    }

    $http.get('/api/groups')
      .then(function (response) {
        angular.copy(response.data, vm.groups)
        console.log(response.data)

        if ($('#logoutButton').length) {
          $http.get('/api/groups/getusergroup') //change to fill in user group template, copy response to newgroup, toggle groupCreated = true
          .then(function (response) {
            //angular.copy(response.data, vm.newGroup);
            //vm.groupCreated = true;
          }, function () {
            
          })
        }

      });

    vm.activityChanged = function () {
      changeDifficulty();
    }

    vm.addGroup = function () {
      makeDescriptionNonEmpty();
      postGroup()
      //add same logic as initial get user group?
    }

    var postGroup = function () {
      return $http.post('/api/groups', vm.newGroup)   
    }

    vm.editGroup = function () {
      vm.newGroup.Id = 21359;
      return $http.post('/api/groups/edit', vm.newGroup);
      //console.log('hi');
    }

    function addGroupFromServer(group) {
      $scope.$apply(function () {
        vm.groups.unshift(group);
        $timeout(function () {
          var firstRow = $('.groupRow').first();
          $(firstRow).addClass("newGroupBorder");
          setTimeout(function () {
            firstRow.css(
            {
              'background-color': '#272b30',
              'transition': 'background-color 1500ms ease-in'
            });
          }, 200);

        }, 0);
      })
    }

    function editOrRefreshGroupFromServer(group) {
      vm.groups = vm.groups.filter(function (existingGroup) {
        return existingGroup.battleTag != group.battleTag;
      });

      vm.groups.unshift(group);
    }

    var changeDifficulty = function () {
      if (vm.newGroup.activity === 'Greater Rifts') {
        vm.newGroup.grLevel = '51-55'
        vm.newGroup.difficulty = '';
        vm.showDifficulty = false;
        vm.greaterRift = true
      } else {
        vm.newGroup.difficulty = 'Torment XI'
        vm.newGroup.grLevel = '';
        vm.showDifficulty = true;
        vm.greaterRift = false;
      }
      if (vm.newGroup.activity === 'Power Leveling') {
        vm.newGroup.difficulty = 'Torment VI';
      }
    }

    var makeDescriptionNonEmpty = function () {
      if (vm.newGroup.description === '') {
        vm.newGroup.description = 'Slaying demons and such.';
      }
    }

    var hub = $.connection.groupHub;
    console.log($.connection.publishGroup)
    hub.client.publishGroup = addGroupFromServer;
    hub.client.editGroup = editOrRefreshGroupFromServer;
    //edit group from server and refresh group from server

    $.connection.hub.logging = true;
    $.connection.hub.start();

  }
})();