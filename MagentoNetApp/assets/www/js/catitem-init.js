TestModel = Backbone.Model.extend({
	defaults:	{
		nameValue : "defaultName",
		descriptionValue : "defaultDescription"
	}
});

ListView = Backbone.View.extend({
  	initialize: function() {
  	},
	render : function () {
		var template = _.template( $("#search_template").html(), this.model.toJSON() );
		this.$el.html(template);
		return this;
	}
});

