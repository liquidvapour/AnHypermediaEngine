# Hyper Multimedia

## Client
* Overview
    * Bootstrap
        * Gets the root response and caches it in the form of a top level menu;
        * Executes the first link in the menu; and
        * Binds the current response.
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
    		* FilmsCollection.js
    		* FilmsCollectionItem.js
    * Sub-state capture
    	* State managing sub-states for a richer user experience (i.e.: Menu, Collections)
    		* Root.js
    		* FilmsCollection.js
    		* FilmsCollectionItem.js

## Food for Thought
* What is the  application.js? What is the root.js? What is the entity.js?
