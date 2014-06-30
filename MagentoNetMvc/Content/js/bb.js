ListView = Backbone.View.extend({
	template: "#listTemplate",

	render : function () {
		var html = "<H1>first backbone test completed</H1>";
		$(this.el).html(html);
		return this;
	}

});

jQuery(function () {
	var view = new ListView();
	$("#production-list").html(view.render().el);
});