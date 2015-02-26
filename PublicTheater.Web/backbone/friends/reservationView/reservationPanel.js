

define([

    'friends/base/view',
    'friends/base/viewBag',
    'friends/facebook/friendCollection',
    'friends/friendSelection/friendSelection',
    'friends/reservationView/templates'

], function (BaseView, ViewBag, FriendCollection, FriendSelection, Templates) {

    return BaseView.extend({

        template: Templates.reservationPanel,

        initialize: function (opts) {
            this.context = opts.context;
            this.performance = opts.performance;

            this.bag = new ViewBag();
            this.bag.raise('loadingFriends');

            this.context.loadUserFriends().then(_(function () {
                this.bag.lower('loadingFriends');

                this.initFriendLists();
                this.context.loadUserFriends().then(_(this.initSelectionView).bind(this));
            }).bind(this));
        },

        initFriendLists: function () {
            this.allFriends = this.context.user.friends;
            this.selectedFriends = new FriendCollection();

            var selectedIds = this.performance.reservation.get('allowedFriendIds');
            var friends = _(selectedIds).map(function (friendId) {
                return this.allFriends.get(friendId);
            }, this);
            this.selectedFriends.add(friends);
        },

        initSelectionView: function () {
            this.selectionView = new FriendSelection({
                selected: this.selectedFriends,
                available: this.allFriends
            });

            this.listenTo(this.selectionView, 'changedSelection', this.didChangeSelection, this);

            this.selectionView.render();
            this.render();
        },

        events: {
            "click [data-action=save]": "didClickSave"
        },

        didClickSave: function (evt) {
            evt.preventDefault();
            this.onSaving();

            this.context.saveReservation(this.performance.reservation)
                .then(_(function (reservation) {

                    console.log(reservation);
                    this.onSaved();

                }).bind(this));
        },

        didChangeSelection: function () {
            var selectedIds = this.selectionView.getSelectedFriendIds();
            this.performance.reservation.set('allowedFriendIds', selectedIds);
            this.onChanged();
        },

        didRender: function () {
            this.$save = this.$el.find('[data-action=save]');
            this.$saving = this.$el.find('[data-bb=saving]');
            this.$saved = this.$el.find('[data-bb=saved]');
            this.onChanged();
            this.selectionView && this.replaceWithView('friendSelection', this.selectionView);
        },

        onSaving: function () {
            this.$save.hide();
            this.$saved.hide();
            this.$saving.show();
        },

        onChanged: function () {
            this.$save.show();
            this.$saved.hide();
            this.$saving.hide();
        },

        onSaved: function () {
            this.$save.hide();
            this.$saved.show();
            this.$saving.hide();
        },

        templateModel: function () {
            return {
                performance: this.performance.toJSON(),
                reservation: this.performance.reservation.toJSON(),
                bag: this.bag.toJSON() 
            };
        }

    });

});