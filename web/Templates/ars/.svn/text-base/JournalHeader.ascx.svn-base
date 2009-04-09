<%@ Control Language="C#" AutoEventWireup="true" %>
	<div class="ContentHeader">
		<ul class="Menu">
			<li><html:HyperLink runat="server" Path="/journals/apple"><html:Image runat="server" src="images/jnav_il.gif" alt="Infinite Loop" /></html:HyperLink></li>
			<li><html:HyperLink runat="server" Path="/journals/thumbs"><html:Image runat="server" src="images/jnav_ot.gif" alt="Opposable Thumbs" /></html:HyperLink></li>
			<li><html:HyperLink runat="server" Path="/journals/microsoft"><html:Image runat="server" src="images/jnav_omw.gif" alt="One Microsoft Way" /></html:HyperLink></li>
			<li><html:HyperLink runat="server" Path="/journals/science"><html:Image runat="server" src="images/jnav_ni.gif" alt="Nobel Intent" /></html:HyperLink></li>
			<li><html:HyperLink runat="server" Path="/journals/hardware"><html:Image runat="server" src="images/jnav_kit.gif" alt="Kit" /></html:HyperLink></li>
			<li><html:HyperLink runat="server" Path="/journals/linux"><html:Image runat="server" src="images/jnav_oe.gif" alt="Open Ended" /></html:HyperLink></li>
		</ul>
		<html:Hyperlink runat="server" class="JournalHeaderLink"><span class="Replace"><data:Field name="Title" runat="server" /></span></html:Hyperlink>
	</div>
	<if:Path StartsWith="/staff" runat='server'>
		<div class="JournalSelector">
		<p>
		Select a Staff journal to view:
		<select class="DropdownNav">
			<option></option>
			<option value="/staff.ars">All Staff Journals</option>
		<data:List runat="server">
			<Data>
				<data:Index OrderBy="SortableTitle">
					<Filters>
						<data:IndexField Name="Path" Value="/staff/*" Mode="Require" />
						<data:IndexField Name="IsContentContainer" Value="true" Mode="Require" />
					</Filters>
				</data:Index>
			</Data>
			<ItemTemplate>
				<data:Field IsPath="true" runat="server" Name="Path" Format='<option value="{0}">' />
				<data:Field runat="server" Name="Title" Format='{0}</option>' />
			</ItemTemplate>
		</data:List>
		</select>
		</p>
		</div>
	</if:Path>
	<div class="ContentBody">