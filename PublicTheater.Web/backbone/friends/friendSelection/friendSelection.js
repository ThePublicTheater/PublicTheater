

define([

    'friends/base/view',
    'friends/facebook/friendCollection',
    'friends/friendSelection/templates'

], function (BaseView, FriendCollection, Templates) {
    "use strict";

    return BaseView.extend({

        className: "friends-share-selection",

        template: Templates.friendSelection,

        initialize: function (opts) {
            this.selected = opts.selected;
            this.available = opts.available;

            this.listenTo(this.selected, 'add remove change reset', this.updateSelected, this);
        },

        events: {
            "click [data-bb=selectedList] > div": "didClickFriend",
            "click [data-bb=availableList] li": "didClickFriend",
            "click [data-bb=cancelButton]": "didClickCancel",
            "click [data-bb=shareButton]": "didClickShare"
        },

        didClickCancel: function (evt) {
            evt.preventDefault();
            this.remove();
            this.delegate.onShareSelectionRemove();
        },

        didClickShare: function (evt) {
            evt.preventDefault();
            this.delegate.onShareSelectionShare(this);
        },

        didClickFriend: function (evt) {
            var friendId = $(evt.currentTarget).attr('data-id');
            var targetFriend = this.available.get(friendId);
            this.selected.toggle(targetFriend);
        },

        didRender: function () {
            this.updateSelected();
        },

        updateSelected: function () {
            this.trigger('changedSelection');
            var html = Templates.selectedList({ friends: this.selected.toJSON() });
            this.$el.find('[data-bb=selectedList]').html(html);
        },

        getSelectedFriendIds: function () {
            return this.selected.pluck('id');
        },

        templateModel: function () {
            return {
                friends: this.available.toJSON()
            };
        }

    });

});