// AJAX Plugin
// Docs Here:
// http://jquery.com/docs/ajax/

/**
 * Load HTML from a remote file and inject it into the DOM, only if it's
 * been modified by the server.
 *
 * @example $("#feeds").loadIfModified("feeds.html")
 * @before <div id="feeds"></div>
 * @result <div id="feeds"><b>45</b> feeds found.</div>
 *
 * @name loadIfModified
 * @type jQuery
 * @param String url The URL of the HTML file to load.
 * @param Hash params A set of key/value pairs that will be sent to the server.
 * @param Function callback A function to be executed whenever the data is loaded.
 * @cat AJAX
 */
jQuery.fn.loadIfModified = function( url, params, callback ) {
	this.load( url, params, callback, 1 );
};

/**
 * Load HTML from a remote file and inject it into the DOM.
 *
 * @example $("#feeds").load("feeds.html")
 * @before <div id="feeds"></div>
 * @result <div id="feeds"><b>45</b> feeds found.</div>
 *
 * @name load
 * @type jQuery
 * @param String url The URL of the HTML file to load.
 * @param Hash params A set of key/value pairs that will be sent to the server.
 * @param Function callback A function to be executed whenever the data is loaded.
 * @cat AJAX
 */
jQuery.fn.load = function( url, params, callback, ifModified ) {
	if ( url.constructor == Function )
		return this.bind("load", url);

	callback = callback || function(){};

	// Default to a GET request
	var type = "GET";

	// If the second parameter was provided
	if ( params ) {
		// If it's a function
		if ( params.constructor == Function ) {
			// We assume that it's the callback
			callback = params;
			params = null;
			
		// Otherwise, build a param string
		} else {
			params = jQuery.param( params );
			type = "POST";
		}
	}
	
	var self = this;
	
	// Request the remote document
	jQuery.ajax( type, url, params,function(res, status){
		
		if ( status == "success" || !ifModified && status == "notmodified" ) {
			// Inject the HTML into all the matched elements
			self.html(res.responseText).each( callback, [res.responseText, status] );
			
			// Execute all the scripts inside of the newly-injected HTML
			$("script", self).each(function(){
				if ( this.src )
					$.getScript( this.src );
				else
					eval.call( window, this.text || this.textContent || this.innerHTML || "" );
			});
		} else
			callback.apply( self, [res.responseText, status] );

	}, ifModified);
	
	return this;
};

/**
 * A function for serializing a set of input elements into
 * a string of data.
 *
 * @example $("input[@type=text]").serialize();
 * @before <input type='text' name='name' value='John'/>
 * <input type='text' name='location' value='Boston'/>
 * @after name=John&location=Boston
 * @desc Serialize a selection of input elements to a string
 *
 * @name serialize
 * @type String
 * @cat AJAX
 */
jQuery.fn.serialize = function(){
	return $.param( this );
};

// If IE is used, create a wrapper for the XMLHttpRequest object
if ( jQuery.browser.msie && typeof XMLHttpRequest == "undefined" )
	XMLHttpRequest = function(){
		return new ActiveXObject(
			navigator.userAgent.indexOf("MSIE 5") >= 0 ?
			"Microsoft.XMLHTTP" : "Msxml2.XMLHTTP"
		);
	};

// Attach a bunch of functions for handling common AJAX events

/**
 * Attach a function to be executed whenever an AJAX request begins.
 *
 * @example $("#loading").ajaxStart(function(){
 *   $(this).show();
 * });
 * @desc Show a loading message whenever an AJAX request starts.
 *
 * @name ajaxStart
 * @type jQuery
 * @param Function callback The function to execute.
 * @cat AJAX
 */
 
/**
 * Attach a function to be executed whenever all AJAX requests have ended.
 *
 * @example $("#loading").ajaxStop(function(){
 *   $(this).hide();
 * });
 * @desc Hide a loading message after all the AJAX requests have stopped.
 *
 * @name ajaxStop
 * @type jQuery
 * @param Function callback The function to execute.
 * @cat AJAX
 */
 
/**
 * Attach a function to be executed whenever an AJAX request completes.
 *
 * @example $("#msg").ajaxComplete(function(){
 *   $(this).append("<li>Request Complete.</li>");
 * });
 * @desc Show a message when an AJAX request completes.
 *
 * @name ajaxComplete
 * @type jQuery
 * @param Function callback The function to execute.
 * @cat AJAX
 */
 
/**
 * Attach a function to be executed whenever an AJAX request completes
 * successfully.
 *
 * @example $("#msg").ajaxSuccess(function(){
 *   $(this).append("<li>Successful Request!</li>");
 * });
 * @desc Show a message when an AJAX request completes successfully.
 *
 * @name ajaxSuccess
 * @type jQuery
 * @param Function callback The function to execute.
 * @cat AJAX
 */
 
/**
 * Attach a function to be executed whenever an AJAX request fails.
 *
 * @example $("#msg").ajaxError(function(){
 *   $(this).append("<li>Error requesting page.</li>");
 * });
 * @desc Show a message when an AJAX request fails.
 *
 * @name ajaxError
 * @type jQuery
 * @param Function callback The function to execute.
 * @cat AJAX
 */

new function(){
	var e = "ajaxStart,ajaxStop,ajaxComplete,ajaxError,ajaxSuccess".split(",");
	
	for ( var i = 0; i < e.length; i++ ) new function(){
		var o = e[i];
		jQuery.fn[o] = function(f){
			return this.bind(o, f);
		};
	};
};

jQuery.extend({

	/**
	 * Load a remote page using an HTTP GET request. All of the arguments to
	 * the method (except URL) are optional.
	 *
	 * @example $.get("test.cgi")
	 *
	 * @example $.get("test.cgi", { name: "John", time: "2pm" } )
	 *
	 * @example $.get("test.cgi", function(data){
	 *   alert("Data Loaded: " + data);
	 * })
	 *
	 * @example $.get("test.cgi",
	 *   { name: "John", time: "2pm" },
	 *   function(data){
	 *     alert("Data Loaded: " + data);
	 *   }
	 * )
	 *
	 * @name $.get
	 * @type jQuery
	 * @param String url The URL of the page to load.
	 * @param Hash params A set of key/value pairs that will be sent to the server.
	 * @param Function callback A function to be executed whenever the data is loaded.
	 * @cat AJAX
	 */
	get: function( url, data, callback, type, ifModified ) {
		if ( data.constructor == Function ) {
			type = callback;
			callback = data;
			data = null;
		}
		
		if ( data ) url += "?" + jQuery.param(data);
		
		// Build and start the HTTP Request
		jQuery.ajax( "GET", url, null, function(r, status) {
			if ( callback ) callback( jQuery.httpData(r,type), status );
		}, ifModified);
	},
	
	/**
	 * Load a remote page using an HTTP GET request, only if it hasn't
	 * been modified since it was last retrieved. All of the arguments to
	 * the method (except URL) are optional.
	 *
	 * @example $.getIfModified("test.html")
	 *
	 * @example $.getIfModified("test.html", { name: "John", time: "2pm" } )
	 *
	 * @example $.getIfModified("test.cgi", function(data){
	 *   alert("Data Loaded: " + data);
	 * })
	 *
	 * @example $.getifModified("test.cgi",
	 *   { name: "John", time: "2pm" },
	 *   function(data){
	 *     alert("Data Loaded: " + data);
	 *   }
	 * )
	 *
	 * @name $.getIfModified
	 * @type jQuery
	 * @param String url The URL of the page to load.
	 * @param Hash params A set of key/value pairs that will be sent to the server.
	 * @param Function callback A function to be executed whenever the data is loaded.
	 * @cat AJAX
	 */
	getIfModified: function( url, data, callback, type ) {
		jQuery.get(url, data, callback, type, 1);
	},

	/**
	 * Loads, and executes, a remote JavaScript file using an HTTP GET request.
	 * All of the arguments to the method (except URL) are optional.
	 *
	 * @example $.getScript("test.js")
	 *
	 * @example $.getScript("test.js", function(){
	 *   alert("Script loaded and executed.");
	 * })
	 *
	 *
	 * @name $.getScript
	 * @type jQuery
	 * @param String url The URL of the page to load.
	 * @param Function callback A function to be executed whenever the data is loaded.
	 * @cat AJAX
	 */
	getScript: function( url, data, callback ) {
		jQuery.get(url, data, callback, "script");
	},
	
	/**
	 * Load a remote JSON object using an HTTP GET request.
	 * All of the arguments to the method (except URL) are optional.
	 *
	 * @example $.getJSON("test.js", function(json){
	 *   alert("JSON Data: " + json.users[3].name);
	 * })
	 *
	 * @example $.getJSON("test.js",
	 *   { name: "John", time: "2pm" },
	 *   function(json){
	 *     alert("JSON Data: " + json.users[3].name);
	 *   }
	 * )
	 *
	 * @name $.getJSON
	 * @type jQuery
	 * @param String url The URL of the page to load.
	 * @param Hash params A set of key/value pairs that will be sent to the server.
	 * @param Function callback A function to be executed whenever the data is loaded.
	 * @cat AJAX
	 */
	getJSON: function( url, data, callback ) {
		jQuery.get(url, data, callback, "json");
	},
	
	/**
	 * Load a remote page using an HTTP POST request. All of the arguments to
	 * the method (except URL) are optional.
	 *
	 * @example $.post("test.cgi")
	 *
	 * @example $.post("test.cgi", { name: "John", time: "2pm" } )
	 *
	 * @example $.post("test.cgi", function(data){
	 *   alert("Data Loaded: " + data);
	 * })
	 *
	 * @example $.post("test.cgi",
	 *   { name: "John", time: "2pm" },
	 *   function(data){
	 *     alert("Data Loaded: " + data);
	 *   }
	 * )
	 *
	 * @name $.post
	 * @type jQuery
	 * @param String url The URL of the page to load.
	 * @param Hash params A set of key/value pairs that will be sent to the server.
	 * @param Function callback A function to be executed whenever the data is loaded.
	 * @cat AJAX
	 */
	post: function( url, data, callback, type ) {
		// Build and start the HTTP Request
		jQuery.ajax( "POST", url, jQuery.param(data), function(r, status) {
			if ( callback ) callback( jQuery.httpData(r,type), status );
		});
	},
	
	// timeout (ms)
	timeout: 0,

	/**
	 * Set the timeout of all AJAX requests to a specific amount of time.
	 * This will make all future AJAX requests timeout after a specified amount
	 * of time (the default is no timeout).
	 *
	 * @example $.ajaxTimeout( 5000 );
	 * @desc Make all AJAX requests timeout after 5 seconds.
	 *
	 * @name $.ajaxTimeout
	 * @type jQuery
	 * @param Number time How long before an AJAX request times out.
	 * @cat AJAX
	 */
	ajaxTimeout: function(timeout) {
		jQuery.timeout = timeout;
	},

	// Last-Modified header cache for next request
	lastModified: {},
	
	/**
	 * Load a remote page using an HTTP request. This function is the primary
	 * means of making AJAX requests using jQuery. $.ajax() takes one property,
	 * an object of key/value pairs, that're are used to initalize the request.
	 *
	 * These are all the key/values that can be passed in to 'prop':
	 *
	 * (String) type - The type of request to make (e.g. "POST" or "GET").
	 *
	 * (String) url - The URL of the page to request.
	 * 
	 * (String) data - A string of data to be sent to the server (POST only).
	 *
	 * (String) dataType - The type of data that you're expecting back from
	 * the server (e.g. "xml", "html", "script", or "json").
	 *
	 * (Function) error - A function to be called if the request fails. The
	 * function gets passed two arguments: The XMLHttpRequest object and a
	 * string describing the type of error that occurred.
	 *
	 * (Function) success - A function to be called if the request succeeds. The
	 * function gets passed one argument: The data returned from the server,
	 * formatted according to the 'dataType' parameter.
	 *
	 * (Function) complete - A function to be called when the request finishes. The
	 * function gets passed two arguments: The XMLHttpRequest object and a
	 * string describing the type the success of the request.
	 *
	 * @example $.ajax({
	 *   type: "GET",
	 *   url: "test.js",
	 *   dataType: "script"
	 * })
	 * @desc Load and execute a JavaScript file.
	 *
	 * @example $.ajax({
	 *   type: "POST",
	 *   url: "some.php",
	 *   data: "name=John&location=Boston",
	 *   success: function(msg){
	 *     alert( "Data Saved: " + msg );
	 *   }
	 * });
	 * @desc Save some data to the server and notify the user once its complete.
	 *
	 * @name $.ajax
	 * @type jQuery
	 * @param Hash prop A set of properties to initialize the request with.
	 * @cat AJAX
	 */
	ajax: function( type, url, data, ret, ifModified ) {
		// If only a single argument was passed in,
		// assume that it is a object of key/value pairs
		if ( !url ) {
			ret = type.complete;
			var success = type.success;
			var error = type.error;
			var dataType = type.dataType;
			data = type.data;
			url = type.url;
			type = type.type;
		}
		
		// Watch for a new set of requests
		if ( ! jQuery.active++ )
			jQuery.event.trigger( "ajaxStart" );

		var requestDone = false;
	
		// Create the request object
		var xml = new XMLHttpRequest();
	
		// Open the socket
		xml.open(type || "GET", url, true);
		
		// Set the correct header, if data is being sent
		if ( data )
			xml.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
		
		// Set the If-Modified-Since header, if ifModified mode.
		if ( ifModified )
			xml.setRequestHeader("If-Modified-Since",
				jQuery.lastModified[url] || "Thu, 01 Jan 1970 00:00:00 GMT" );
		
		// Set header so calling script knows that it's an XMLHttpRequest
		xml.setRequestHeader("X-Requested-With", "XMLHttpRequest");
	
		// Make sure the browser sends the right content length
		if ( xml.overrideMimeType )
			xml.setRequestHeader("Connection", "close");
		
		// Wait for a response to come back
		var onreadystatechange = function(istimeout){
			// The transfer is complete and the data is available, or the request timed out
			if ( xml && (xml.readyState == 4 || istimeout == "timeout") ) {
				requestDone = true;

				var status = jQuery.httpSuccess( xml ) && istimeout != "timeout" ?
					ifModified && jQuery.httpNotModified( xml, url ) ? "notmodified" : "success" : "error";
				
				// Make sure that the request was successful or notmodified
				if ( status != "error" ) {
					// Cache Last-Modified header, if ifModified mode.
					var modRes = xml.getResponseHeader("Last-Modified");
					if ( ifModified && modRes ) jQuery.lastModified[url] = modRes;
					
					// If a local callback was specified, fire it
					if ( success )
						success( jQuery.httpData( xml, dataType ), status );
					
					// Fire the global callback
					jQuery.event.trigger( "ajaxSuccess" );
				
				// Otherwise, the request was not successful
				} else {
					// If a local callback was specified, fire it
					if ( error ) error( xml, status );
					
					// Fire the global callback
					jQuery.event.trigger( "ajaxError" );
				}
				
				// The request was completed
				jQuery.event.trigger( "ajaxComplete" );
				
				// Handle the global AJAX counter
				if ( ! --jQuery.active )
					jQuery.event.trigger( "ajaxStop" );
	
				// Process result
				if ( ret ) ret(xml, status);
				
				// Stop memory leaks
				xml.onreadystatechange = function(){};
				xml = null;
				
			}
		};
		xml.onreadystatechange = onreadystatechange;
		
		// Timeout checker
		if(jQuery.timeout > 0)
			setTimeout(function(){
				// Check to see if the request is still happening
				if (xml) {
					// Cancel the request
					xml.abort();

					if ( !requestDone ) onreadystatechange( "timeout" );

					// Clear from memory
					xml = null;
				}
			}, jQuery.timeout);
		
		// Send the data
		xml.send(data);
	},
	
	// Counter for holding the number of active queries
	active: 0,
	
	// Determines if an XMLHttpRequest was successful or not
	httpSuccess: function(r) {
		try {
			return !r.status && location.protocol == "file:" ||
				( r.status >= 200 && r.status < 300 ) || r.status == 304 ||
				jQuery.browser.safari && r.status == undefined;
		} catch(e){}

		return false;
	},

	// Determines if an XMLHttpRequest returns NotModified
	httpNotModified: function(xml, url) {
		try {
			var xmlRes = xml.getResponseHeader("Last-Modified");

			// Firefox always returns 200. check Last-Modified date
			return xml.status == 304 || xmlRes == jQuery.lastModified[url] ||
				jQuery.browser.safari && xml.status == undefined;
		} catch(e){}

		return false;
	},
	
	/* Get the data out of an XMLHttpRequest.
	 * Return parsed XML if content-type header is "xml" and type is "xml" or omitted,
	 * otherwise return plain text.
	 * (String) data - The type of data that you're expecting back,
	 * (e.g. "xml", "html", "script")
	 */
	httpData: function(r,type) {
		var ct = r.getResponseHeader("content-type");
		var data = !type && ct && ct.indexOf("xml") >= 0;
		data = type == "xml" || data ? r.responseXML : r.responseText;

		// If the type is "script", eval it
		if ( type == "script" ) eval.call( window, data );

		// Get the JavaScript object, if JSON is used.
		if ( type == "json" ) eval( "data = " + data );

		return data;
	},
	
	// Serialize an array of form elements or a set of
	// key/values into a query string
	param: function(a) {
		var s = [];
		
		// If an array was passed in, assume that it is an array
		// of form elements
		if ( a.constructor == Array || a.jquery ) {
			// Serialize the form elements
			for ( var i = 0; i < a.length; i++ )
				s.push( a[i].name + "=" + encodeURIComponent( a[i].value ) );
			
		// Otherwise, assume that it's an object of key/value pairs
		} else {
			// Serialize the key/values
			for ( var j in a )
				s.push( j + "=" + encodeURIComponent( a[j] ) );
		}
		
		// Return the resulting serialization
		return s.join("&");
	}

});
