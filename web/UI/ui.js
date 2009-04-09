        var expandos = Array();
        function checkAll(check, names){
            checks = document.getElementsByName(names);
            for(i = 0; i < checks.length; i++){
                if(checks[i].type == 'checkbox'){
                    checks[i].checked = check.checked;
                }
            }
        }
        function toggleParent(link, state1, state2){
            block = link.parentNode;
            block.className = (block.className == state1) ? state2 : state1;
        }
        function savePost(){
            tags = checkTags();
            if(!tags){
                alert('No tags selected.  Please choose two or more now.');
                block = document.getElementById('TagSelector');
                block.className = 'Expanded';
                return false;
            }
        }
        function checkTags(){
            checks = document.getElementsByName('tag');
            for(i = 0; i < checks.length; i++){
                if(checks[i].checked) return true;
            }
            return false;
        }
