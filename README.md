# Hyper Multimedia

## Client
* Overview
    * Bootstrap
        * Gets the root response and caches it in the form of a top level menu;
        * Executes the first link in the menu; and
        * Binds the current response.
    * Transitions
    	* Link/action execution results in a new message that bubbles up through the current response object graph;
    	* The parent object may choose to react to the message and/or bubble up to the next layer; and
    	* The application is the last layer with the default behaviour of rebinding the current response.
    		* The application also handles unexpected errors as exceptions (i.e.: no state transition).
    * Extensions
    	* Styles (i.e.: Books) 
	    	* BooksCollection.css
	    	* Book.css
    	* Layout (i.e.: Paintings)
	    	* PaintingsCollection.cshtml
	    	* PaintingsCollectionItem.cshtml
    	* Behaviour (i.e.: Films)
	    	* FilmsCollection.js
	    	* FilmsCollectionItem.js
* Patterns
    * Load/unload authenticated extensions
    	* Client reload on authentication actions (full page reload or javascript driven)
	    	* Login: Unauthorised.js
	    	* Logout: Root.js
    * State self refresh
    	* State that self refreshes on successful write operations
    		* Entity.js
    		* CollectionEntity.js
    		* FilmsCollection.js
    		* FilmsCollectionItem.js
    * Action state
    	* State that only has links/actions so an action is selected as active by default (i.e.: Login/Register/Reset Password)
    		* ActionEntity.js
    * Sub-state capture
    	* State managing sub-states for a richer user experience (i.e.: Menu, Collections)
    		* Root.js
    		* FilmsCollection.js
    		* FilmsCollectionItem.js

## To-do
* Client
    * Refresh of root response when cache expires
    * Html for remaining field types
    * Browser controls override (back/forward)
    * Partial views
* Server
    * Http cache headers
    * Collection filters
	    * Filter fields lookup (autocomplete, filtered options, ...)
    * Testing patterns


## Food for Thought
* What is the  application.js? What is the root.js? What is the entity.js?
