var ownerId = '';
var updating = false;


$(window).load(function () {
    $(function () {
        var Todo = function (title, done, order, callback,id) {
            var self = this;
            self.origTitle = title;
            self.origDone = done;
            self.title = ko.observable(title);
            self.done = ko.observable(done);
            self.order = order;
            self.id = id;
            self.updateCallback = ko.computed(function () {
                callback(self);
                return true;
            });
        }
        ownerId = document.cookie.toString();
        ownerId = ownerId.replace('user=','');
        diPSClient.Connect('ws://localhost:8888/dips');
        var viewModel = function () {
            var self = this;
            self.todos = ko.observableArray([]);
            self.inputTitle = ko.observable("");
            self.doneTodos = ko.observable(0);
            self.markAll = ko.observable(false);

            self.addOne = function () {
                var order = 1;
                if (self.todos().length > 0)
                    order = self.todos()[self.todos().length-1].order + 1;
                var tsk = { OwnerId: ownerId, Description: self.inputTitle(), Completed: false, Order: order };
                diPSClient.Publish('InsertTask', tsk);
            };

            self.createOnEnter = function (item, event) {
                if (event.keyCode == 13 && self.inputTitle()) {
                    self.addOne();
                    self.inputTitle("");
                } else {
                    return true;
                };
            }

            self.toggleEditMode = function (item, event) {
                $(event.target).closest('li').toggleClass('editing');
            }

            self.editOnEnter = function (item, event) {
                if (event.keyCode == 13 && item.title) {
                    item.updateCallback();
                    self.toggleEditMode(item, event);
                } else {
                    return true;
                };
            }

            self.markAll.subscribe(function (newValue) {
                ko.utils.arrayForEach(self.todos(), function (item) {
                    return item.done(newValue);
                });
            });

            self.countUpdate = function (item) {
                var doneArray = ko.utils.arrayFilter(self.todos(), function (it) {
                    return it.done();
                });
                self.doneTodos(doneArray.length);
                //update the item on the server only if it has changes
                if (!updating && (item.title() != item.origTitle || item.done() != item.origDone )) {
                    var tsk = { Id: item.id, OwnerId: ownerId, Description: item.title(), Completed: item.done() };
                    diPSClient.Publish('UpdateTask', tsk);
                }
                return true;
            };

            self.countDoneText = function (bool) {
                var cntAll = self.todos().length;
                var cnt = (bool ? self.doneTodos() : cntAll - self.doneTodos());
                var text = "<span class='count'>" + cnt.toString() + "</span>";
                text += (bool ? " completed" : " remaining");
                text += (self.doneTodos() > 1 ? " items" : " item");
                return text;
            }

            self.clear = function () {
                ko.utils.arrayForEach(self.todos(), function (item) {
                    if (item.done()) {
                        diPSClient.Publish('DeleteTask', {Id : item.id });
                    }
                });
                //reload the list
                diPSClient.Publish('GetTasks', { UserId: ownerId });
            }
        };

        var todosVM = new viewModel();
        
        //Receives the task list from the server
        diPSClient.Subscribe("TaskListFromServer", function (data) {
            //flag
            updating = true;
            todosVM.todos.removeAll();
            //add all 
            for (xi in data) {
                var t = new Todo(data[xi].Description, data[xi].Completed, data[xi].ViewOrder, todosVM.countUpdate, data[xi].Id);
                todosVM.todos.push(t);               
            }
            //This is needed because knockout may fire the change events a little bit slow
            setTimeout(function () { updating = false; }, 250);
        });
        

        ko.applyBindings(todosVM);
        //Get the list
        diPSClient.Publish('GetTasks', { UserId: ownerId });
        
    })

});
