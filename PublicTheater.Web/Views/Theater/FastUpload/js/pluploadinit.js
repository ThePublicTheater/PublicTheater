// Convert divs to queue widgets when the DOM is ready
$(function() {
	$("#uploader").pluploadQueue({
		// General settings
		runtimes: 'flash,html4',
		url: '/Plupload/plupload.ashx?toGallery=' + GetUploadDir()+'&userId='+GetUserId(),
		max_file_size: '10mb',
        chunk_size : '1mb',
		max_file_count: 20, // user can add no more then 20 files at a time
		unique_names: true,
		// Sort files
		sortable: true,

		// Specify what files to browse for
		
		filters: [
			{ title: "Image files", extensions: "jpg,gif,png" }
//			{ title: "Zip files", extensions: "zip,avi" },
//			{ title: "Pdf files", extensions: "pdf" },
//			{ title: "Text files", extensions: "txt" }
		],
         init : { 
                 UploadComplete: function(up, files) {
                     UpdateImages();
                }
        },
		
		// Flash settings
		flash_swf_url: '/Plupload/js/plupload.flash.swf'

	});
//    $('#uploader').plupload('getUploader').bind('FilesAdded', function(up, files) {
//        
//        up.start();
//    });
//	var uploader = $('#uploader').pluploadQueue();
//	uploader.bind('UploadComplete', function(up, files) {
//		UpdateImages();

//	   
//	});

	// Client side form validation
	$('form').submit(function(e) {
		var uploader = $('#uploader').pluploadQueue();

		// Validate number of uploaded files
			// Files in queue upload them first
			if (uploader.files.length > 0) {
				// When all files are uploaded submit form
				uploader.bind('StateChanged', function() {
					if (uploader.files.length === (uploader.total.uploaded + uploader.total.failed)) {
						$('form').submit();
					 }
				});
				uploader.start();
			} else
				alert('You must at least upload one file.');

			e.preventDefault();
	  
	});

});
