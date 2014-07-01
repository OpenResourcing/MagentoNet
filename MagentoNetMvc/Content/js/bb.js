TestModel = Backbone.Model.extend({
	defaults:	{
		nameValue : "defaultName",
		ageValue : "defaultAge"
	}
});

ListView = Backbone.View.extend({
 	events: {
    	"click #search_button" : "select",
    	"click #age" : "select2"
  	},
  	initialize: function() {
	  	this.model.on('change', this.render, this);
  	},
	render : function () {
		var template = _.template( $("#search_template").html(), this.model.toJSON() );
		this.$el.html(template);
		this.$el.css("background-color","blue");
		this.$(".redclass").css("background-color","red");
		return this;
	},
	select : function (){
		this.model.set({"ageValue" : "40"});
	},
	select2 : function (){
		this.model.set({"nameValue" : "changed!"});
	}

});

jQuery(function () {
	var mymodel = new TestModel ({nameValue : "newName", ageValue : "newAge"});
	var view = new ListView({model : mymodel});
	$("#production-list").html(view.render().el);
});