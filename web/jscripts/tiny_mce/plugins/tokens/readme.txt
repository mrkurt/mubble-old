tokens plugin
This plugin was created to insert 'tokens' (for my case, DB-replaceable text tokens).
It's extremely simple, but it does what I need it to.

Usage:
plugins:"tokens",
theme_advanced_buttons[x]:"tokens"
tokens_token_list:"Token1=$token1$;Token2=$token2$"

In this example, a drop-down box will appear that will allow you to chose either Token1 or Token2
When one is selected, the appropriate text will be inserted into the editor.
It works just like the 'theme_advanced_styles' option.
Simple!
