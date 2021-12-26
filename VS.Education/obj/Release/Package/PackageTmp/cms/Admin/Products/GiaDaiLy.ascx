<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GiaDaiLy.ascx.cs" Inherits="VS.E_Commerce.cms.Admin.Products.GiaDaiLy" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<div id="cph_Main_ContentPane">
    <div id="">
        <div class="Block Breadcrumb ui-widget-content ui-corner-top ui-corner-bottom" id="Breadcrumb">
            <ul>
                <li class="SecondLast"><a href="/admin.aspx"><i style="font-size: 14px;" class="icon-home"></i>Trang chủ</a></li>
                <li class="Last"><span>Quản lý giá đại lý</span></li>
            </ul>
        </div>
        <div style="clear: both;"></div>
        <div class="widget">

            <div class="widget-title">
                <h4 style="color: red; font-weight: 600"><i class="icon-list-alt"></i>&nbsp;CHI TIẾT SẢN PHẨM</h4>
            </div>
            <div class="widget-body">
                <div class="list_item">
                    <asp:Repeater ID="rpitems" runat="server">
                        <ItemTemplate>
                            <tr style="background-color: #f1f1f1" height="40">
                                <td style="text-align: center;">
                                  <a title="<%#Eval("Name")%>" target="_blank" href="/<%#Eval("TangName")%>.html">  <%#MoreAll.MoreImage.Image(Eval("ImagesSmall").ToString())%></a>
                                </td>
                                <td style="text-align: center;">
                                    <a title="<%#Eval("Name")%>" target="_blank" href="/<%#Eval("TangName")%>.html"><b><%#Eval("Name")%></b></a>


                                </td>
                                <td style="text-align: center;">
                                    <%#AllQuery.MorePro.FormatMoney(Eval("Price").ToString())%>
                                </td>
                                <td style="text-align: center;">
                                    <%#MoreAll.MoreAll.FormatDate(DataBinder.Eval(Container.DataItem, "Create_Date"))%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <table cellspacing="0" style="border-collapse: collapse; margin-top: 18px" class="table table-striped table-bordered dataTable table-hover">
                                <tr class="trHeader" style="height: 25px">
                                    <td style="text-align: center;" class="header" align="left">Hình ảnh</td>
                                    <td style="text-align: center;" class="header">Tên sản phẩm</td>
                                    <td style="text-align: center;" class="header">Giá</td>
                                    <td style="text-align: center;" class="header">Ngày tạo</td>
                                </tr>
                        </HeaderTemplate>
                        <FooterTemplate>
                            </TABLE>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>

            </div>
        </div>
    </div>
</div>
<asp:Panel ID="pn_list" runat="server" Width="100%"></asp:Panel>
<asp:Label ID="lbl_curpage" runat="server" Font-Bold="True" ForeColor="Red" Visible="True"></asp:Label>
<asp:Literal ID="ltmsg" runat="server"></asp:Literal>
<div class="widget">
    <div class="widget-title">
        <h4 style="color: red; font-weight: 600"><i class="icon-list-alt"></i>&nbsp;QUẢN LÝ GIÁ THEO SỐ LƯỢNG CỦA ĐẠI LÝ</h4>
    </div>
    <div class="widget-body">
        <div class="list_item">
            <asp:Repeater ID="rp_pagelist" OnItemCommand="rp_pagelist_ItemCommand" runat="server" OnItemDataBound="rp_pagelist_ItemDataBound">
                <ItemTemplate>
                    <tr style="background-color: #f1f1f1" height="40">
                        <td style="text-align: center;">
                            <asp:HiddenField ID="hiID" Value='<%# Eval("ID") %>' runat="server" />
                             <asp:HiddenField ID="hdSoLuongTu" Value='<%#Eval("SoLuongTu") %>' runat="server" />
                             <asp:HiddenField ID="hdSoLuongDen" Value='<%#Eval("SoLuongDen") %>' runat="server" />
                            <asp:DropDownList ID="ddlsoluongtu" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlsoluongtu_SelectedIndexChanged" Width="144px">
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                                <asp:ListItem Value="6">6</asp:ListItem>
                                <asp:ListItem Value="7">7</asp:ListItem>
                                <asp:ListItem Value="8">8</asp:ListItem>
                                <asp:ListItem Value="9">9</asp:ListItem>
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="11">11</asp:ListItem>
                                <asp:ListItem Value="12">12</asp:ListItem>
                                <asp:ListItem Value="13">13</asp:ListItem>
                                <asp:ListItem Value="14">14</asp:ListItem>
                                <asp:ListItem Value="15">15</asp:ListItem>
                                <asp:ListItem Value="16">16</asp:ListItem>
                                <asp:ListItem Value="17">17</asp:ListItem>
                                <asp:ListItem Value="18">18</asp:ListItem>
                                <asp:ListItem Value="19">19</asp:ListItem>
                                <asp:ListItem Value="20">20</asp:ListItem>
                                <asp:ListItem Value="21">21</asp:ListItem>
                                <asp:ListItem Value="22">22</asp:ListItem>
                                <asp:ListItem Value="23">23</asp:ListItem>
                                <asp:ListItem Value="24">24</asp:ListItem>
                                <asp:ListItem Value="25">25</asp:ListItem>
                                <asp:ListItem Value="26">26</asp:ListItem>
                                <asp:ListItem Value="27">27</asp:ListItem>
                                <asp:ListItem Value="28">28</asp:ListItem>
                                <asp:ListItem Value="29">29</asp:ListItem>
                                <asp:ListItem Value="30">30</asp:ListItem>
                                <asp:ListItem Value="31">31</asp:ListItem>
                                <asp:ListItem Value="32">32</asp:ListItem>
                                <asp:ListItem Value="33">33</asp:ListItem>
                                <asp:ListItem Value="34">34</asp:ListItem>
                                <asp:ListItem Value="35">35</asp:ListItem>
                                <asp:ListItem Value="36">36</asp:ListItem>
                                <asp:ListItem Value="37">37</asp:ListItem>
                                <asp:ListItem Value="38">38</asp:ListItem>
                                <asp:ListItem Value="39">39</asp:ListItem>
                                <asp:ListItem Value="40">40</asp:ListItem>
                                <asp:ListItem Value="41">41</asp:ListItem>
                                <asp:ListItem Value="42">42</asp:ListItem>
                                <asp:ListItem Value="43">43</asp:ListItem>
                                <asp:ListItem Value="44">44</asp:ListItem>
                                <asp:ListItem Value="45">45</asp:ListItem>
                                <asp:ListItem Value="46">46</asp:ListItem>
                                <asp:ListItem Value="47">47</asp:ListItem>
                                <asp:ListItem Value="48">48</asp:ListItem>
                                <asp:ListItem Value="49">49</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                                <asp:ListItem Value="51">51</asp:ListItem>
                                <asp:ListItem Value="52">52</asp:ListItem>
                                <asp:ListItem Value="53">53</asp:ListItem>
                                <asp:ListItem Value="54">54</asp:ListItem>
                                <asp:ListItem Value="55">55</asp:ListItem>
                                <asp:ListItem Value="56">56</asp:ListItem>
                                <asp:ListItem Value="57">57</asp:ListItem>
                                <asp:ListItem Value="58">58</asp:ListItem>
                                <asp:ListItem Value="59">59</asp:ListItem>
                                <asp:ListItem Value="60">60</asp:ListItem>
                                <asp:ListItem Value="61">61</asp:ListItem>
                                <asp:ListItem Value="62">62</asp:ListItem>
                                <asp:ListItem Value="63">63</asp:ListItem>
                                <asp:ListItem Value="64">64</asp:ListItem>
                                <asp:ListItem Value="65">65</asp:ListItem>
                                <asp:ListItem Value="66">66</asp:ListItem>
                                <asp:ListItem Value="67">67</asp:ListItem>
                                <asp:ListItem Value="68">68</asp:ListItem>
                                <asp:ListItem Value="69">69</asp:ListItem>
                                <asp:ListItem Value="70">70</asp:ListItem>
                                <asp:ListItem Value="71">71</asp:ListItem>
                                <asp:ListItem Value="72">72</asp:ListItem>
                                <asp:ListItem Value="73">73</asp:ListItem>
                                <asp:ListItem Value="74">74</asp:ListItem>
                                <asp:ListItem Value="75">75</asp:ListItem>
                                <asp:ListItem Value="76">76</asp:ListItem>
                                <asp:ListItem Value="77">77</asp:ListItem>
                                <asp:ListItem Value="78">78</asp:ListItem>
                                <asp:ListItem Value="79">79</asp:ListItem>
                                <asp:ListItem Value="80">80</asp:ListItem>
                                <asp:ListItem Value="81">81</asp:ListItem>
                                <asp:ListItem Value="82">82</asp:ListItem>
                                <asp:ListItem Value="83">83</asp:ListItem>
                                <asp:ListItem Value="84">84</asp:ListItem>
                                <asp:ListItem Value="85">85</asp:ListItem>
                                <asp:ListItem Value="86">86</asp:ListItem>
                                <asp:ListItem Value="87">87</asp:ListItem>
                                <asp:ListItem Value="88">88</asp:ListItem>
                                <asp:ListItem Value="89">89</asp:ListItem>
                                <asp:ListItem Value="90">90</asp:ListItem>
                                <asp:ListItem Value="91">91</asp:ListItem>
                                <asp:ListItem Value="92">92</asp:ListItem>
                                <asp:ListItem Value="93">93</asp:ListItem>
                                <asp:ListItem Value="94">94</asp:ListItem>
                                <asp:ListItem Value="95">95</asp:ListItem>
                                <asp:ListItem Value="96">96</asp:ListItem>
                                <asp:ListItem Value="97">97</asp:ListItem>
                                <asp:ListItem Value="98">98</asp:ListItem>
                                <asp:ListItem Value="99">99</asp:ListItem>
                                <asp:ListItem Value="100">100</asp:ListItem>
                                <asp:ListItem Value="101">101</asp:ListItem>
                                <asp:ListItem Value="102">102</asp:ListItem>
                                <asp:ListItem Value="103">103</asp:ListItem>
                                <asp:ListItem Value="104">104</asp:ListItem>
                                <asp:ListItem Value="105">105</asp:ListItem>
                                <asp:ListItem Value="106">106</asp:ListItem>
                                <asp:ListItem Value="107">107</asp:ListItem>
                                <asp:ListItem Value="108">108</asp:ListItem>
                                <asp:ListItem Value="109">109</asp:ListItem>
                                <asp:ListItem Value="110">110</asp:ListItem>
                                <asp:ListItem Value="111">111</asp:ListItem>
                                <asp:ListItem Value="112">112</asp:ListItem>
                                <asp:ListItem Value="113">113</asp:ListItem>
                                <asp:ListItem Value="114">114</asp:ListItem>
                                <asp:ListItem Value="115">115</asp:ListItem>
                                <asp:ListItem Value="116">116</asp:ListItem>
                                <asp:ListItem Value="117">117</asp:ListItem>
                                <asp:ListItem Value="118">118</asp:ListItem>
                                <asp:ListItem Value="119">119</asp:ListItem>
                                <asp:ListItem Value="120">120</asp:ListItem>
                                <asp:ListItem Value="121">121</asp:ListItem>
                                <asp:ListItem Value="122">122</asp:ListItem>
                                <asp:ListItem Value="123">123</asp:ListItem>
                                <asp:ListItem Value="124">124</asp:ListItem>
                                <asp:ListItem Value="125">125</asp:ListItem>
                                <asp:ListItem Value="126">126</asp:ListItem>
                                <asp:ListItem Value="127">127</asp:ListItem>
                                <asp:ListItem Value="128">128</asp:ListItem>
                                <asp:ListItem Value="129">129</asp:ListItem>
                                <asp:ListItem Value="130">130</asp:ListItem>
                                <asp:ListItem Value="131">131</asp:ListItem>
                                <asp:ListItem Value="132">132</asp:ListItem>
                                <asp:ListItem Value="133">133</asp:ListItem>
                                <asp:ListItem Value="134">134</asp:ListItem>
                                <asp:ListItem Value="135">135</asp:ListItem>
                                <asp:ListItem Value="136">136</asp:ListItem>
                                <asp:ListItem Value="137">137</asp:ListItem>
                                <asp:ListItem Value="138">138</asp:ListItem>
                                <asp:ListItem Value="139">139</asp:ListItem>
                                <asp:ListItem Value="140">140</asp:ListItem>
                                <asp:ListItem Value="141">141</asp:ListItem>
                                <asp:ListItem Value="142">142</asp:ListItem>
                                <asp:ListItem Value="143">143</asp:ListItem>
                                <asp:ListItem Value="144">144</asp:ListItem>
                                <asp:ListItem Value="145">145</asp:ListItem>
                                <asp:ListItem Value="146">146</asp:ListItem>
                                <asp:ListItem Value="147">147</asp:ListItem>
                                <asp:ListItem Value="148">148</asp:ListItem>
                                <asp:ListItem Value="149">149</asp:ListItem>
                                <asp:ListItem Value="150">150</asp:ListItem>
                                <asp:ListItem Value="151">151</asp:ListItem>
                                <asp:ListItem Value="152">152</asp:ListItem>
                                <asp:ListItem Value="153">153</asp:ListItem>
                                <asp:ListItem Value="154">154</asp:ListItem>
                                <asp:ListItem Value="155">155</asp:ListItem>
                                <asp:ListItem Value="156">156</asp:ListItem>
                                <asp:ListItem Value="157">157</asp:ListItem>
                                <asp:ListItem Value="158">158</asp:ListItem>
                                <asp:ListItem Value="159">159</asp:ListItem>
                                <asp:ListItem Value="160">160</asp:ListItem>
                                <asp:ListItem Value="161">161</asp:ListItem>
                                <asp:ListItem Value="162">162</asp:ListItem>
                                <asp:ListItem Value="163">163</asp:ListItem>
                                <asp:ListItem Value="164">164</asp:ListItem>
                                <asp:ListItem Value="165">165</asp:ListItem>
                                <asp:ListItem Value="166">166</asp:ListItem>
                                <asp:ListItem Value="167">167</asp:ListItem>
                                <asp:ListItem Value="168">168</asp:ListItem>
                                <asp:ListItem Value="169">169</asp:ListItem>
                                <asp:ListItem Value="170">170</asp:ListItem>
                                <asp:ListItem Value="171">171</asp:ListItem>
                                <asp:ListItem Value="172">172</asp:ListItem>
                                <asp:ListItem Value="173">173</asp:ListItem>
                                <asp:ListItem Value="174">174</asp:ListItem>
                                <asp:ListItem Value="175">175</asp:ListItem>
                                <asp:ListItem Value="176">176</asp:ListItem>
                                <asp:ListItem Value="177">177</asp:ListItem>
                                <asp:ListItem Value="178">178</asp:ListItem>
                                <asp:ListItem Value="179">179</asp:ListItem>
                                <asp:ListItem Value="180">180</asp:ListItem>
                                <asp:ListItem Value="181">181</asp:ListItem>
                                <asp:ListItem Value="182">182</asp:ListItem>
                                <asp:ListItem Value="183">183</asp:ListItem>
                                <asp:ListItem Value="184">184</asp:ListItem>
                                <asp:ListItem Value="185">185</asp:ListItem>
                                <asp:ListItem Value="186">186</asp:ListItem>
                                <asp:ListItem Value="187">187</asp:ListItem>
                                <asp:ListItem Value="188">188</asp:ListItem>
                                <asp:ListItem Value="189">189</asp:ListItem>
                                <asp:ListItem Value="190">190</asp:ListItem>
                                <asp:ListItem Value="191">191</asp:ListItem>
                                <asp:ListItem Value="192">192</asp:ListItem>
                                <asp:ListItem Value="193">193</asp:ListItem>
                                <asp:ListItem Value="194">194</asp:ListItem>
                                <asp:ListItem Value="195">195</asp:ListItem>
                                <asp:ListItem Value="196">196</asp:ListItem>
                                <asp:ListItem Value="197">197</asp:ListItem>
                                <asp:ListItem Value="198">198</asp:ListItem>
                                <asp:ListItem Value="199">199</asp:ListItem>
                                <asp:ListItem Value="200">200</asp:ListItem>
                                <asp:ListItem Value="201">201</asp:ListItem>
                                <asp:ListItem Value="202">202</asp:ListItem>
                                <asp:ListItem Value="203">203</asp:ListItem>
                                <asp:ListItem Value="204">204</asp:ListItem>
                                <asp:ListItem Value="205">205</asp:ListItem>
                                <asp:ListItem Value="206">206</asp:ListItem>
                                <asp:ListItem Value="207">207</asp:ListItem>
                                <asp:ListItem Value="208">208</asp:ListItem>
                                <asp:ListItem Value="209">209</asp:ListItem>
                                <asp:ListItem Value="210">210</asp:ListItem>
                                <asp:ListItem Value="211">211</asp:ListItem>
                                <asp:ListItem Value="212">212</asp:ListItem>
                                <asp:ListItem Value="213">213</asp:ListItem>
                                <asp:ListItem Value="214">214</asp:ListItem>
                                <asp:ListItem Value="215">215</asp:ListItem>
                                <asp:ListItem Value="216">216</asp:ListItem>
                                <asp:ListItem Value="217">217</asp:ListItem>
                                <asp:ListItem Value="218">218</asp:ListItem>
                                <asp:ListItem Value="219">219</asp:ListItem>
                                <asp:ListItem Value="220">220</asp:ListItem>
                                <asp:ListItem Value="221">221</asp:ListItem>
                                <asp:ListItem Value="222">222</asp:ListItem>
                                <asp:ListItem Value="223">223</asp:ListItem>
                                <asp:ListItem Value="224">224</asp:ListItem>
                                <asp:ListItem Value="225">225</asp:ListItem>
                                <asp:ListItem Value="226">226</asp:ListItem>
                                <asp:ListItem Value="227">227</asp:ListItem>
                                <asp:ListItem Value="228">228</asp:ListItem>
                                <asp:ListItem Value="229">229</asp:ListItem>
                                <asp:ListItem Value="230">230</asp:ListItem>
                                <asp:ListItem Value="231">231</asp:ListItem>
                                <asp:ListItem Value="232">232</asp:ListItem>
                                <asp:ListItem Value="233">233</asp:ListItem>
                                <asp:ListItem Value="234">234</asp:ListItem>
                                <asp:ListItem Value="235">235</asp:ListItem>
                                <asp:ListItem Value="236">236</asp:ListItem>
                                <asp:ListItem Value="237">237</asp:ListItem>
                                <asp:ListItem Value="238">238</asp:ListItem>
                                <asp:ListItem Value="239">239</asp:ListItem>
                                <asp:ListItem Value="240">240</asp:ListItem>
                                <asp:ListItem Value="241">241</asp:ListItem>
                                <asp:ListItem Value="242">242</asp:ListItem>
                                <asp:ListItem Value="243">243</asp:ListItem>
                                <asp:ListItem Value="244">244</asp:ListItem>
                                <asp:ListItem Value="245">245</asp:ListItem>
                                <asp:ListItem Value="246">246</asp:ListItem>
                                <asp:ListItem Value="247">247</asp:ListItem>
                                <asp:ListItem Value="248">248</asp:ListItem>
                                <asp:ListItem Value="249">249</asp:ListItem>
                                <asp:ListItem Value="250">250</asp:ListItem>
                                <asp:ListItem Value="251">251</asp:ListItem>
                                <asp:ListItem Value="252">252</asp:ListItem>
                                <asp:ListItem Value="253">253</asp:ListItem>
                                <asp:ListItem Value="254">254</asp:ListItem>
                                <asp:ListItem Value="255">255</asp:ListItem>
                                <asp:ListItem Value="256">256</asp:ListItem>
                                <asp:ListItem Value="257">257</asp:ListItem>
                                <asp:ListItem Value="258">258</asp:ListItem>
                                <asp:ListItem Value="259">259</asp:ListItem>
                                <asp:ListItem Value="260">260</asp:ListItem>
                                <asp:ListItem Value="261">261</asp:ListItem>
                                <asp:ListItem Value="262">262</asp:ListItem>
                                <asp:ListItem Value="263">263</asp:ListItem>
                                <asp:ListItem Value="264">264</asp:ListItem>
                                <asp:ListItem Value="265">265</asp:ListItem>
                                <asp:ListItem Value="266">266</asp:ListItem>
                                <asp:ListItem Value="267">267</asp:ListItem>
                                <asp:ListItem Value="268">268</asp:ListItem>
                                <asp:ListItem Value="269">269</asp:ListItem>
                                <asp:ListItem Value="270">270</asp:ListItem>
                                <asp:ListItem Value="271">271</asp:ListItem>
                                <asp:ListItem Value="272">272</asp:ListItem>
                                <asp:ListItem Value="273">273</asp:ListItem>
                                <asp:ListItem Value="274">274</asp:ListItem>
                                <asp:ListItem Value="275">275</asp:ListItem>
                                <asp:ListItem Value="276">276</asp:ListItem>
                                <asp:ListItem Value="277">277</asp:ListItem>
                                <asp:ListItem Value="278">278</asp:ListItem>
                                <asp:ListItem Value="279">279</asp:ListItem>
                                <asp:ListItem Value="280">280</asp:ListItem>
                                <asp:ListItem Value="281">281</asp:ListItem>
                                <asp:ListItem Value="282">282</asp:ListItem>
                                <asp:ListItem Value="283">283</asp:ListItem>
                                <asp:ListItem Value="284">284</asp:ListItem>
                                <asp:ListItem Value="285">285</asp:ListItem>
                                <asp:ListItem Value="286">286</asp:ListItem>
                                <asp:ListItem Value="287">287</asp:ListItem>
                                <asp:ListItem Value="288">288</asp:ListItem>
                                <asp:ListItem Value="289">289</asp:ListItem>
                                <asp:ListItem Value="290">290</asp:ListItem>
                                <asp:ListItem Value="291">291</asp:ListItem>
                                <asp:ListItem Value="292">292</asp:ListItem>
                                <asp:ListItem Value="293">293</asp:ListItem>
                                <asp:ListItem Value="294">294</asp:ListItem>
                                <asp:ListItem Value="295">295</asp:ListItem>
                                <asp:ListItem Value="296">296</asp:ListItem>
                                <asp:ListItem Value="297">297</asp:ListItem>
                                <asp:ListItem Value="298">298</asp:ListItem>
                                <asp:ListItem Value="299">299</asp:ListItem>
                                <asp:ListItem Value="300">300</asp:ListItem>
                                <asp:ListItem Value="301">301</asp:ListItem>
                                <asp:ListItem Value="302">302</asp:ListItem>
                                <asp:ListItem Value="303">303</asp:ListItem>
                                <asp:ListItem Value="304">304</asp:ListItem>
                                <asp:ListItem Value="305">305</asp:ListItem>
                                <asp:ListItem Value="306">306</asp:ListItem>
                                <asp:ListItem Value="307">307</asp:ListItem>
                                <asp:ListItem Value="308">308</asp:ListItem>
                                <asp:ListItem Value="309">309</asp:ListItem>
                                <asp:ListItem Value="310">310</asp:ListItem>
                                <asp:ListItem Value="311">311</asp:ListItem>
                                <asp:ListItem Value="312">312</asp:ListItem>
                                <asp:ListItem Value="313">313</asp:ListItem>
                                <asp:ListItem Value="314">314</asp:ListItem>
                                <asp:ListItem Value="315">315</asp:ListItem>
                                <asp:ListItem Value="316">316</asp:ListItem>
                                <asp:ListItem Value="317">317</asp:ListItem>
                                <asp:ListItem Value="318">318</asp:ListItem>
                                <asp:ListItem Value="319">319</asp:ListItem>
                                <asp:ListItem Value="320">320</asp:ListItem>
                                <asp:ListItem Value="321">321</asp:ListItem>
                                <asp:ListItem Value="322">322</asp:ListItem>
                                <asp:ListItem Value="323">323</asp:ListItem>
                                <asp:ListItem Value="324">324</asp:ListItem>
                                <asp:ListItem Value="325">325</asp:ListItem>
                                <asp:ListItem Value="326">326</asp:ListItem>
                                <asp:ListItem Value="327">327</asp:ListItem>
                                <asp:ListItem Value="328">328</asp:ListItem>
                                <asp:ListItem Value="329">329</asp:ListItem>
                                <asp:ListItem Value="330">330</asp:ListItem>
                                <asp:ListItem Value="331">331</asp:ListItem>
                                <asp:ListItem Value="332">332</asp:ListItem>
                                <asp:ListItem Value="333">333</asp:ListItem>
                                <asp:ListItem Value="334">334</asp:ListItem>
                                <asp:ListItem Value="335">335</asp:ListItem>
                                <asp:ListItem Value="336">336</asp:ListItem>
                                <asp:ListItem Value="337">337</asp:ListItem>
                                <asp:ListItem Value="338">338</asp:ListItem>
                                <asp:ListItem Value="339">339</asp:ListItem>
                                <asp:ListItem Value="340">340</asp:ListItem>
                                <asp:ListItem Value="341">341</asp:ListItem>
                                <asp:ListItem Value="342">342</asp:ListItem>
                                <asp:ListItem Value="343">343</asp:ListItem>
                                <asp:ListItem Value="344">344</asp:ListItem>
                                <asp:ListItem Value="345">345</asp:ListItem>
                                <asp:ListItem Value="346">346</asp:ListItem>
                                <asp:ListItem Value="347">347</asp:ListItem>
                                <asp:ListItem Value="348">348</asp:ListItem>
                                <asp:ListItem Value="349">349</asp:ListItem>
                                <asp:ListItem Value="350">350</asp:ListItem>
                                <asp:ListItem Value="351">351</asp:ListItem>
                                <asp:ListItem Value="352">352</asp:ListItem>
                                <asp:ListItem Value="353">353</asp:ListItem>
                                <asp:ListItem Value="354">354</asp:ListItem>
                                <asp:ListItem Value="355">355</asp:ListItem>
                                <asp:ListItem Value="356">356</asp:ListItem>
                                <asp:ListItem Value="357">357</asp:ListItem>
                                <asp:ListItem Value="358">358</asp:ListItem>
                                <asp:ListItem Value="359">359</asp:ListItem>
                                <asp:ListItem Value="360">360</asp:ListItem>
                                <asp:ListItem Value="361">361</asp:ListItem>
                                <asp:ListItem Value="362">362</asp:ListItem>
                                <asp:ListItem Value="363">363</asp:ListItem>
                                <asp:ListItem Value="364">364</asp:ListItem>
                                <asp:ListItem Value="365">365</asp:ListItem>
                                <asp:ListItem Value="366">366</asp:ListItem>
                                <asp:ListItem Value="367">367</asp:ListItem>
                                <asp:ListItem Value="368">368</asp:ListItem>
                                <asp:ListItem Value="369">369</asp:ListItem>
                                <asp:ListItem Value="370">370</asp:ListItem>
                                <asp:ListItem Value="371">371</asp:ListItem>
                                <asp:ListItem Value="372">372</asp:ListItem>
                                <asp:ListItem Value="373">373</asp:ListItem>
                                <asp:ListItem Value="374">374</asp:ListItem>
                                <asp:ListItem Value="375">375</asp:ListItem>
                                <asp:ListItem Value="376">376</asp:ListItem>
                                <asp:ListItem Value="377">377</asp:ListItem>
                                <asp:ListItem Value="378">378</asp:ListItem>
                                <asp:ListItem Value="379">379</asp:ListItem>
                                <asp:ListItem Value="380">380</asp:ListItem>
                                <asp:ListItem Value="381">381</asp:ListItem>
                                <asp:ListItem Value="382">382</asp:ListItem>
                                <asp:ListItem Value="383">383</asp:ListItem>
                                <asp:ListItem Value="384">384</asp:ListItem>
                                <asp:ListItem Value="385">385</asp:ListItem>
                                <asp:ListItem Value="386">386</asp:ListItem>
                                <asp:ListItem Value="387">387</asp:ListItem>
                                <asp:ListItem Value="388">388</asp:ListItem>
                                <asp:ListItem Value="389">389</asp:ListItem>
                                <asp:ListItem Value="390">390</asp:ListItem>
                                <asp:ListItem Value="391">391</asp:ListItem>
                                <asp:ListItem Value="392">392</asp:ListItem>
                                <asp:ListItem Value="393">393</asp:ListItem>
                                <asp:ListItem Value="394">394</asp:ListItem>
                                <asp:ListItem Value="395">395</asp:ListItem>
                                <asp:ListItem Value="396">396</asp:ListItem>
                                <asp:ListItem Value="397">397</asp:ListItem>
                                <asp:ListItem Value="398">398</asp:ListItem>
                                <asp:ListItem Value="399">399</asp:ListItem>
                                <asp:ListItem Value="400">400</asp:ListItem>
                                <asp:ListItem Value="401">401</asp:ListItem>
                                <asp:ListItem Value="402">402</asp:ListItem>
                                <asp:ListItem Value="403">403</asp:ListItem>
                                <asp:ListItem Value="404">404</asp:ListItem>
                                <asp:ListItem Value="405">405</asp:ListItem>
                                <asp:ListItem Value="406">406</asp:ListItem>
                                <asp:ListItem Value="407">407</asp:ListItem>
                                <asp:ListItem Value="408">408</asp:ListItem>
                                <asp:ListItem Value="409">409</asp:ListItem>
                                <asp:ListItem Value="410">410</asp:ListItem>
                                <asp:ListItem Value="411">411</asp:ListItem>
                                <asp:ListItem Value="412">412</asp:ListItem>
                                <asp:ListItem Value="413">413</asp:ListItem>
                                <asp:ListItem Value="414">414</asp:ListItem>
                                <asp:ListItem Value="415">415</asp:ListItem>
                                <asp:ListItem Value="416">416</asp:ListItem>
                                <asp:ListItem Value="417">417</asp:ListItem>
                                <asp:ListItem Value="418">418</asp:ListItem>
                                <asp:ListItem Value="419">419</asp:ListItem>
                                <asp:ListItem Value="420">420</asp:ListItem>
                                <asp:ListItem Value="421">421</asp:ListItem>
                                <asp:ListItem Value="422">422</asp:ListItem>
                                <asp:ListItem Value="423">423</asp:ListItem>
                                <asp:ListItem Value="424">424</asp:ListItem>
                                <asp:ListItem Value="425">425</asp:ListItem>
                                <asp:ListItem Value="426">426</asp:ListItem>
                                <asp:ListItem Value="427">427</asp:ListItem>
                                <asp:ListItem Value="428">428</asp:ListItem>
                                <asp:ListItem Value="429">429</asp:ListItem>
                                <asp:ListItem Value="430">430</asp:ListItem>
                                <asp:ListItem Value="431">431</asp:ListItem>
                                <asp:ListItem Value="432">432</asp:ListItem>
                                <asp:ListItem Value="433">433</asp:ListItem>
                                <asp:ListItem Value="434">434</asp:ListItem>
                                <asp:ListItem Value="435">435</asp:ListItem>
                                <asp:ListItem Value="436">436</asp:ListItem>
                                <asp:ListItem Value="437">437</asp:ListItem>
                                <asp:ListItem Value="438">438</asp:ListItem>
                                <asp:ListItem Value="439">439</asp:ListItem>
                                <asp:ListItem Value="440">440</asp:ListItem>
                                <asp:ListItem Value="441">441</asp:ListItem>
                                <asp:ListItem Value="442">442</asp:ListItem>
                                <asp:ListItem Value="443">443</asp:ListItem>
                                <asp:ListItem Value="444">444</asp:ListItem>
                                <asp:ListItem Value="445">445</asp:ListItem>
                                <asp:ListItem Value="446">446</asp:ListItem>
                                <asp:ListItem Value="447">447</asp:ListItem>
                                <asp:ListItem Value="448">448</asp:ListItem>
                                <asp:ListItem Value="449">449</asp:ListItem>
                                <asp:ListItem Value="450">450</asp:ListItem>
                                <asp:ListItem Value="451">451</asp:ListItem>
                                <asp:ListItem Value="452">452</asp:ListItem>
                                <asp:ListItem Value="453">453</asp:ListItem>
                                <asp:ListItem Value="454">454</asp:ListItem>
                                <asp:ListItem Value="455">455</asp:ListItem>
                                <asp:ListItem Value="456">456</asp:ListItem>
                                <asp:ListItem Value="457">457</asp:ListItem>
                                <asp:ListItem Value="458">458</asp:ListItem>
                                <asp:ListItem Value="459">459</asp:ListItem>
                                <asp:ListItem Value="460">460</asp:ListItem>
                                <asp:ListItem Value="461">461</asp:ListItem>
                                <asp:ListItem Value="462">462</asp:ListItem>
                                <asp:ListItem Value="463">463</asp:ListItem>
                                <asp:ListItem Value="464">464</asp:ListItem>
                                <asp:ListItem Value="465">465</asp:ListItem>
                                <asp:ListItem Value="466">466</asp:ListItem>
                                <asp:ListItem Value="467">467</asp:ListItem>
                                <asp:ListItem Value="468">468</asp:ListItem>
                                <asp:ListItem Value="469">469</asp:ListItem>
                                <asp:ListItem Value="470">470</asp:ListItem>
                                <asp:ListItem Value="471">471</asp:ListItem>
                                <asp:ListItem Value="472">472</asp:ListItem>
                                <asp:ListItem Value="473">473</asp:ListItem>
                                <asp:ListItem Value="474">474</asp:ListItem>
                                <asp:ListItem Value="475">475</asp:ListItem>
                                <asp:ListItem Value="476">476</asp:ListItem>
                                <asp:ListItem Value="477">477</asp:ListItem>
                                <asp:ListItem Value="478">478</asp:ListItem>
                                <asp:ListItem Value="479">479</asp:ListItem>
                                <asp:ListItem Value="480">480</asp:ListItem>
                                <asp:ListItem Value="481">481</asp:ListItem>
                                <asp:ListItem Value="482">482</asp:ListItem>
                                <asp:ListItem Value="483">483</asp:ListItem>
                                <asp:ListItem Value="484">484</asp:ListItem>
                                <asp:ListItem Value="485">485</asp:ListItem>
                                <asp:ListItem Value="486">486</asp:ListItem>
                                <asp:ListItem Value="487">487</asp:ListItem>
                                <asp:ListItem Value="488">488</asp:ListItem>
                                <asp:ListItem Value="489">489</asp:ListItem>
                                <asp:ListItem Value="490">490</asp:ListItem>
                                <asp:ListItem Value="491">491</asp:ListItem>
                                <asp:ListItem Value="492">492</asp:ListItem>
                                <asp:ListItem Value="493">493</asp:ListItem>
                                <asp:ListItem Value="494">494</asp:ListItem>
                                <asp:ListItem Value="495">495</asp:ListItem>
                                <asp:ListItem Value="496">496</asp:ListItem>
                                <asp:ListItem Value="497">497</asp:ListItem>
                                <asp:ListItem Value="498">498</asp:ListItem>
                                <asp:ListItem Value="499">499</asp:ListItem>
                            </asp:DropDownList>

                        <asp:DropDownList ID="ddlsoluongden" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlsoluongden_SelectedIndexChanged" Width="144px">
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                                <asp:ListItem Value="6">6</asp:ListItem>
                                <asp:ListItem Value="7">7</asp:ListItem>
                                <asp:ListItem Value="8">8</asp:ListItem>
                                <asp:ListItem Value="9">9</asp:ListItem>
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="11">11</asp:ListItem>
                                <asp:ListItem Value="12">12</asp:ListItem>
                                <asp:ListItem Value="13">13</asp:ListItem>
                                <asp:ListItem Value="14">14</asp:ListItem>
                                <asp:ListItem Value="15">15</asp:ListItem>
                                <asp:ListItem Value="16">16</asp:ListItem>
                                <asp:ListItem Value="17">17</asp:ListItem>
                                <asp:ListItem Value="18">18</asp:ListItem>
                                <asp:ListItem Value="19">19</asp:ListItem>
                                <asp:ListItem Value="20">20</asp:ListItem>
                                <asp:ListItem Value="21">21</asp:ListItem>
                                <asp:ListItem Value="22">22</asp:ListItem>
                                <asp:ListItem Value="23">23</asp:ListItem>
                                <asp:ListItem Value="24">24</asp:ListItem>
                                <asp:ListItem Value="25">25</asp:ListItem>
                                <asp:ListItem Value="26">26</asp:ListItem>
                                <asp:ListItem Value="27">27</asp:ListItem>
                                <asp:ListItem Value="28">28</asp:ListItem>
                                <asp:ListItem Value="29">29</asp:ListItem>
                                <asp:ListItem Value="30">30</asp:ListItem>
                                <asp:ListItem Value="31">31</asp:ListItem>
                                <asp:ListItem Value="32">32</asp:ListItem>
                                <asp:ListItem Value="33">33</asp:ListItem>
                                <asp:ListItem Value="34">34</asp:ListItem>
                                <asp:ListItem Value="35">35</asp:ListItem>
                                <asp:ListItem Value="36">36</asp:ListItem>
                                <asp:ListItem Value="37">37</asp:ListItem>
                                <asp:ListItem Value="38">38</asp:ListItem>
                                <asp:ListItem Value="39">39</asp:ListItem>
                                <asp:ListItem Value="40">40</asp:ListItem>
                                <asp:ListItem Value="41">41</asp:ListItem>
                                <asp:ListItem Value="42">42</asp:ListItem>
                                <asp:ListItem Value="43">43</asp:ListItem>
                                <asp:ListItem Value="44">44</asp:ListItem>
                                <asp:ListItem Value="45">45</asp:ListItem>
                                <asp:ListItem Value="46">46</asp:ListItem>
                                <asp:ListItem Value="47">47</asp:ListItem>
                                <asp:ListItem Value="48">48</asp:ListItem>
                                <asp:ListItem Value="49">49</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                                <asp:ListItem Value="51">51</asp:ListItem>
                                <asp:ListItem Value="52">52</asp:ListItem>
                                <asp:ListItem Value="53">53</asp:ListItem>
                                <asp:ListItem Value="54">54</asp:ListItem>
                                <asp:ListItem Value="55">55</asp:ListItem>
                                <asp:ListItem Value="56">56</asp:ListItem>
                                <asp:ListItem Value="57">57</asp:ListItem>
                                <asp:ListItem Value="58">58</asp:ListItem>
                                <asp:ListItem Value="59">59</asp:ListItem>
                                <asp:ListItem Value="60">60</asp:ListItem>
                                <asp:ListItem Value="61">61</asp:ListItem>
                                <asp:ListItem Value="62">62</asp:ListItem>
                                <asp:ListItem Value="63">63</asp:ListItem>
                                <asp:ListItem Value="64">64</asp:ListItem>
                                <asp:ListItem Value="65">65</asp:ListItem>
                                <asp:ListItem Value="66">66</asp:ListItem>
                                <asp:ListItem Value="67">67</asp:ListItem>
                                <asp:ListItem Value="68">68</asp:ListItem>
                                <asp:ListItem Value="69">69</asp:ListItem>
                                <asp:ListItem Value="70">70</asp:ListItem>
                                <asp:ListItem Value="71">71</asp:ListItem>
                                <asp:ListItem Value="72">72</asp:ListItem>
                                <asp:ListItem Value="73">73</asp:ListItem>
                                <asp:ListItem Value="74">74</asp:ListItem>
                                <asp:ListItem Value="75">75</asp:ListItem>
                                <asp:ListItem Value="76">76</asp:ListItem>
                                <asp:ListItem Value="77">77</asp:ListItem>
                                <asp:ListItem Value="78">78</asp:ListItem>
                                <asp:ListItem Value="79">79</asp:ListItem>
                                <asp:ListItem Value="80">80</asp:ListItem>
                                <asp:ListItem Value="81">81</asp:ListItem>
                                <asp:ListItem Value="82">82</asp:ListItem>
                                <asp:ListItem Value="83">83</asp:ListItem>
                                <asp:ListItem Value="84">84</asp:ListItem>
                                <asp:ListItem Value="85">85</asp:ListItem>
                                <asp:ListItem Value="86">86</asp:ListItem>
                                <asp:ListItem Value="87">87</asp:ListItem>
                                <asp:ListItem Value="88">88</asp:ListItem>
                                <asp:ListItem Value="89">89</asp:ListItem>
                                <asp:ListItem Value="90">90</asp:ListItem>
                                <asp:ListItem Value="91">91</asp:ListItem>
                                <asp:ListItem Value="92">92</asp:ListItem>
                                <asp:ListItem Value="93">93</asp:ListItem>
                                <asp:ListItem Value="94">94</asp:ListItem>
                                <asp:ListItem Value="95">95</asp:ListItem>
                                <asp:ListItem Value="96">96</asp:ListItem>
                                <asp:ListItem Value="97">97</asp:ListItem>
                                <asp:ListItem Value="98">98</asp:ListItem>
                                <asp:ListItem Value="99">99</asp:ListItem>
                                <asp:ListItem Value="100">100</asp:ListItem>
                                <asp:ListItem Value="101">101</asp:ListItem>
                                <asp:ListItem Value="102">102</asp:ListItem>
                                <asp:ListItem Value="103">103</asp:ListItem>
                                <asp:ListItem Value="104">104</asp:ListItem>
                                <asp:ListItem Value="105">105</asp:ListItem>
                                <asp:ListItem Value="106">106</asp:ListItem>
                                <asp:ListItem Value="107">107</asp:ListItem>
                                <asp:ListItem Value="108">108</asp:ListItem>
                                <asp:ListItem Value="109">109</asp:ListItem>
                                <asp:ListItem Value="110">110</asp:ListItem>
                                <asp:ListItem Value="111">111</asp:ListItem>
                                <asp:ListItem Value="112">112</asp:ListItem>
                                <asp:ListItem Value="113">113</asp:ListItem>
                                <asp:ListItem Value="114">114</asp:ListItem>
                                <asp:ListItem Value="115">115</asp:ListItem>
                                <asp:ListItem Value="116">116</asp:ListItem>
                                <asp:ListItem Value="117">117</asp:ListItem>
                                <asp:ListItem Value="118">118</asp:ListItem>
                                <asp:ListItem Value="119">119</asp:ListItem>
                                <asp:ListItem Value="120">120</asp:ListItem>
                                <asp:ListItem Value="121">121</asp:ListItem>
                                <asp:ListItem Value="122">122</asp:ListItem>
                                <asp:ListItem Value="123">123</asp:ListItem>
                                <asp:ListItem Value="124">124</asp:ListItem>
                                <asp:ListItem Value="125">125</asp:ListItem>
                                <asp:ListItem Value="126">126</asp:ListItem>
                                <asp:ListItem Value="127">127</asp:ListItem>
                                <asp:ListItem Value="128">128</asp:ListItem>
                                <asp:ListItem Value="129">129</asp:ListItem>
                                <asp:ListItem Value="130">130</asp:ListItem>
                                <asp:ListItem Value="131">131</asp:ListItem>
                                <asp:ListItem Value="132">132</asp:ListItem>
                                <asp:ListItem Value="133">133</asp:ListItem>
                                <asp:ListItem Value="134">134</asp:ListItem>
                                <asp:ListItem Value="135">135</asp:ListItem>
                                <asp:ListItem Value="136">136</asp:ListItem>
                                <asp:ListItem Value="137">137</asp:ListItem>
                                <asp:ListItem Value="138">138</asp:ListItem>
                                <asp:ListItem Value="139">139</asp:ListItem>
                                <asp:ListItem Value="140">140</asp:ListItem>
                                <asp:ListItem Value="141">141</asp:ListItem>
                                <asp:ListItem Value="142">142</asp:ListItem>
                                <asp:ListItem Value="143">143</asp:ListItem>
                                <asp:ListItem Value="144">144</asp:ListItem>
                                <asp:ListItem Value="145">145</asp:ListItem>
                                <asp:ListItem Value="146">146</asp:ListItem>
                                <asp:ListItem Value="147">147</asp:ListItem>
                                <asp:ListItem Value="148">148</asp:ListItem>
                                <asp:ListItem Value="149">149</asp:ListItem>
                                <asp:ListItem Value="150">150</asp:ListItem>
                                <asp:ListItem Value="151">151</asp:ListItem>
                                <asp:ListItem Value="152">152</asp:ListItem>
                                <asp:ListItem Value="153">153</asp:ListItem>
                                <asp:ListItem Value="154">154</asp:ListItem>
                                <asp:ListItem Value="155">155</asp:ListItem>
                                <asp:ListItem Value="156">156</asp:ListItem>
                                <asp:ListItem Value="157">157</asp:ListItem>
                                <asp:ListItem Value="158">158</asp:ListItem>
                                <asp:ListItem Value="159">159</asp:ListItem>
                                <asp:ListItem Value="160">160</asp:ListItem>
                                <asp:ListItem Value="161">161</asp:ListItem>
                                <asp:ListItem Value="162">162</asp:ListItem>
                                <asp:ListItem Value="163">163</asp:ListItem>
                                <asp:ListItem Value="164">164</asp:ListItem>
                                <asp:ListItem Value="165">165</asp:ListItem>
                                <asp:ListItem Value="166">166</asp:ListItem>
                                <asp:ListItem Value="167">167</asp:ListItem>
                                <asp:ListItem Value="168">168</asp:ListItem>
                                <asp:ListItem Value="169">169</asp:ListItem>
                                <asp:ListItem Value="170">170</asp:ListItem>
                                <asp:ListItem Value="171">171</asp:ListItem>
                                <asp:ListItem Value="172">172</asp:ListItem>
                                <asp:ListItem Value="173">173</asp:ListItem>
                                <asp:ListItem Value="174">174</asp:ListItem>
                                <asp:ListItem Value="175">175</asp:ListItem>
                                <asp:ListItem Value="176">176</asp:ListItem>
                                <asp:ListItem Value="177">177</asp:ListItem>
                                <asp:ListItem Value="178">178</asp:ListItem>
                                <asp:ListItem Value="179">179</asp:ListItem>
                                <asp:ListItem Value="180">180</asp:ListItem>
                                <asp:ListItem Value="181">181</asp:ListItem>
                                <asp:ListItem Value="182">182</asp:ListItem>
                                <asp:ListItem Value="183">183</asp:ListItem>
                                <asp:ListItem Value="184">184</asp:ListItem>
                                <asp:ListItem Value="185">185</asp:ListItem>
                                <asp:ListItem Value="186">186</asp:ListItem>
                                <asp:ListItem Value="187">187</asp:ListItem>
                                <asp:ListItem Value="188">188</asp:ListItem>
                                <asp:ListItem Value="189">189</asp:ListItem>
                                <asp:ListItem Value="190">190</asp:ListItem>
                                <asp:ListItem Value="191">191</asp:ListItem>
                                <asp:ListItem Value="192">192</asp:ListItem>
                                <asp:ListItem Value="193">193</asp:ListItem>
                                <asp:ListItem Value="194">194</asp:ListItem>
                                <asp:ListItem Value="195">195</asp:ListItem>
                                <asp:ListItem Value="196">196</asp:ListItem>
                                <asp:ListItem Value="197">197</asp:ListItem>
                                <asp:ListItem Value="198">198</asp:ListItem>
                                <asp:ListItem Value="199">199</asp:ListItem>
                                <asp:ListItem Value="200">200</asp:ListItem>
                                <asp:ListItem Value="201">201</asp:ListItem>
                                <asp:ListItem Value="202">202</asp:ListItem>
                                <asp:ListItem Value="203">203</asp:ListItem>
                                <asp:ListItem Value="204">204</asp:ListItem>
                                <asp:ListItem Value="205">205</asp:ListItem>
                                <asp:ListItem Value="206">206</asp:ListItem>
                                <asp:ListItem Value="207">207</asp:ListItem>
                                <asp:ListItem Value="208">208</asp:ListItem>
                                <asp:ListItem Value="209">209</asp:ListItem>
                                <asp:ListItem Value="210">210</asp:ListItem>
                                <asp:ListItem Value="211">211</asp:ListItem>
                                <asp:ListItem Value="212">212</asp:ListItem>
                                <asp:ListItem Value="213">213</asp:ListItem>
                                <asp:ListItem Value="214">214</asp:ListItem>
                                <asp:ListItem Value="215">215</asp:ListItem>
                                <asp:ListItem Value="216">216</asp:ListItem>
                                <asp:ListItem Value="217">217</asp:ListItem>
                                <asp:ListItem Value="218">218</asp:ListItem>
                                <asp:ListItem Value="219">219</asp:ListItem>
                                <asp:ListItem Value="220">220</asp:ListItem>
                                <asp:ListItem Value="221">221</asp:ListItem>
                                <asp:ListItem Value="222">222</asp:ListItem>
                                <asp:ListItem Value="223">223</asp:ListItem>
                                <asp:ListItem Value="224">224</asp:ListItem>
                                <asp:ListItem Value="225">225</asp:ListItem>
                                <asp:ListItem Value="226">226</asp:ListItem>
                                <asp:ListItem Value="227">227</asp:ListItem>
                                <asp:ListItem Value="228">228</asp:ListItem>
                                <asp:ListItem Value="229">229</asp:ListItem>
                                <asp:ListItem Value="230">230</asp:ListItem>
                                <asp:ListItem Value="231">231</asp:ListItem>
                                <asp:ListItem Value="232">232</asp:ListItem>
                                <asp:ListItem Value="233">233</asp:ListItem>
                                <asp:ListItem Value="234">234</asp:ListItem>
                                <asp:ListItem Value="235">235</asp:ListItem>
                                <asp:ListItem Value="236">236</asp:ListItem>
                                <asp:ListItem Value="237">237</asp:ListItem>
                                <asp:ListItem Value="238">238</asp:ListItem>
                                <asp:ListItem Value="239">239</asp:ListItem>
                                <asp:ListItem Value="240">240</asp:ListItem>
                                <asp:ListItem Value="241">241</asp:ListItem>
                                <asp:ListItem Value="242">242</asp:ListItem>
                                <asp:ListItem Value="243">243</asp:ListItem>
                                <asp:ListItem Value="244">244</asp:ListItem>
                                <asp:ListItem Value="245">245</asp:ListItem>
                                <asp:ListItem Value="246">246</asp:ListItem>
                                <asp:ListItem Value="247">247</asp:ListItem>
                                <asp:ListItem Value="248">248</asp:ListItem>
                                <asp:ListItem Value="249">249</asp:ListItem>
                                <asp:ListItem Value="250">250</asp:ListItem>
                                <asp:ListItem Value="251">251</asp:ListItem>
                                <asp:ListItem Value="252">252</asp:ListItem>
                                <asp:ListItem Value="253">253</asp:ListItem>
                                <asp:ListItem Value="254">254</asp:ListItem>
                                <asp:ListItem Value="255">255</asp:ListItem>
                                <asp:ListItem Value="256">256</asp:ListItem>
                                <asp:ListItem Value="257">257</asp:ListItem>
                                <asp:ListItem Value="258">258</asp:ListItem>
                                <asp:ListItem Value="259">259</asp:ListItem>
                                <asp:ListItem Value="260">260</asp:ListItem>
                                <asp:ListItem Value="261">261</asp:ListItem>
                                <asp:ListItem Value="262">262</asp:ListItem>
                                <asp:ListItem Value="263">263</asp:ListItem>
                                <asp:ListItem Value="264">264</asp:ListItem>
                                <asp:ListItem Value="265">265</asp:ListItem>
                                <asp:ListItem Value="266">266</asp:ListItem>
                                <asp:ListItem Value="267">267</asp:ListItem>
                                <asp:ListItem Value="268">268</asp:ListItem>
                                <asp:ListItem Value="269">269</asp:ListItem>
                                <asp:ListItem Value="270">270</asp:ListItem>
                                <asp:ListItem Value="271">271</asp:ListItem>
                                <asp:ListItem Value="272">272</asp:ListItem>
                                <asp:ListItem Value="273">273</asp:ListItem>
                                <asp:ListItem Value="274">274</asp:ListItem>
                                <asp:ListItem Value="275">275</asp:ListItem>
                                <asp:ListItem Value="276">276</asp:ListItem>
                                <asp:ListItem Value="277">277</asp:ListItem>
                                <asp:ListItem Value="278">278</asp:ListItem>
                                <asp:ListItem Value="279">279</asp:ListItem>
                                <asp:ListItem Value="280">280</asp:ListItem>
                                <asp:ListItem Value="281">281</asp:ListItem>
                                <asp:ListItem Value="282">282</asp:ListItem>
                                <asp:ListItem Value="283">283</asp:ListItem>
                                <asp:ListItem Value="284">284</asp:ListItem>
                                <asp:ListItem Value="285">285</asp:ListItem>
                                <asp:ListItem Value="286">286</asp:ListItem>
                                <asp:ListItem Value="287">287</asp:ListItem>
                                <asp:ListItem Value="288">288</asp:ListItem>
                                <asp:ListItem Value="289">289</asp:ListItem>
                                <asp:ListItem Value="290">290</asp:ListItem>
                                <asp:ListItem Value="291">291</asp:ListItem>
                                <asp:ListItem Value="292">292</asp:ListItem>
                                <asp:ListItem Value="293">293</asp:ListItem>
                                <asp:ListItem Value="294">294</asp:ListItem>
                                <asp:ListItem Value="295">295</asp:ListItem>
                                <asp:ListItem Value="296">296</asp:ListItem>
                                <asp:ListItem Value="297">297</asp:ListItem>
                                <asp:ListItem Value="298">298</asp:ListItem>
                                <asp:ListItem Value="299">299</asp:ListItem>
                                <asp:ListItem Value="300">300</asp:ListItem>
                                <asp:ListItem Value="301">301</asp:ListItem>
                                <asp:ListItem Value="302">302</asp:ListItem>
                                <asp:ListItem Value="303">303</asp:ListItem>
                                <asp:ListItem Value="304">304</asp:ListItem>
                                <asp:ListItem Value="305">305</asp:ListItem>
                                <asp:ListItem Value="306">306</asp:ListItem>
                                <asp:ListItem Value="307">307</asp:ListItem>
                                <asp:ListItem Value="308">308</asp:ListItem>
                                <asp:ListItem Value="309">309</asp:ListItem>
                                <asp:ListItem Value="310">310</asp:ListItem>
                                <asp:ListItem Value="311">311</asp:ListItem>
                                <asp:ListItem Value="312">312</asp:ListItem>
                                <asp:ListItem Value="313">313</asp:ListItem>
                                <asp:ListItem Value="314">314</asp:ListItem>
                                <asp:ListItem Value="315">315</asp:ListItem>
                                <asp:ListItem Value="316">316</asp:ListItem>
                                <asp:ListItem Value="317">317</asp:ListItem>
                                <asp:ListItem Value="318">318</asp:ListItem>
                                <asp:ListItem Value="319">319</asp:ListItem>
                                <asp:ListItem Value="320">320</asp:ListItem>
                                <asp:ListItem Value="321">321</asp:ListItem>
                                <asp:ListItem Value="322">322</asp:ListItem>
                                <asp:ListItem Value="323">323</asp:ListItem>
                                <asp:ListItem Value="324">324</asp:ListItem>
                                <asp:ListItem Value="325">325</asp:ListItem>
                                <asp:ListItem Value="326">326</asp:ListItem>
                                <asp:ListItem Value="327">327</asp:ListItem>
                                <asp:ListItem Value="328">328</asp:ListItem>
                                <asp:ListItem Value="329">329</asp:ListItem>
                                <asp:ListItem Value="330">330</asp:ListItem>
                                <asp:ListItem Value="331">331</asp:ListItem>
                                <asp:ListItem Value="332">332</asp:ListItem>
                                <asp:ListItem Value="333">333</asp:ListItem>
                                <asp:ListItem Value="334">334</asp:ListItem>
                                <asp:ListItem Value="335">335</asp:ListItem>
                                <asp:ListItem Value="336">336</asp:ListItem>
                                <asp:ListItem Value="337">337</asp:ListItem>
                                <asp:ListItem Value="338">338</asp:ListItem>
                                <asp:ListItem Value="339">339</asp:ListItem>
                                <asp:ListItem Value="340">340</asp:ListItem>
                                <asp:ListItem Value="341">341</asp:ListItem>
                                <asp:ListItem Value="342">342</asp:ListItem>
                                <asp:ListItem Value="343">343</asp:ListItem>
                                <asp:ListItem Value="344">344</asp:ListItem>
                                <asp:ListItem Value="345">345</asp:ListItem>
                                <asp:ListItem Value="346">346</asp:ListItem>
                                <asp:ListItem Value="347">347</asp:ListItem>
                                <asp:ListItem Value="348">348</asp:ListItem>
                                <asp:ListItem Value="349">349</asp:ListItem>
                                <asp:ListItem Value="350">350</asp:ListItem>
                                <asp:ListItem Value="351">351</asp:ListItem>
                                <asp:ListItem Value="352">352</asp:ListItem>
                                <asp:ListItem Value="353">353</asp:ListItem>
                                <asp:ListItem Value="354">354</asp:ListItem>
                                <asp:ListItem Value="355">355</asp:ListItem>
                                <asp:ListItem Value="356">356</asp:ListItem>
                                <asp:ListItem Value="357">357</asp:ListItem>
                                <asp:ListItem Value="358">358</asp:ListItem>
                                <asp:ListItem Value="359">359</asp:ListItem>
                                <asp:ListItem Value="360">360</asp:ListItem>
                                <asp:ListItem Value="361">361</asp:ListItem>
                                <asp:ListItem Value="362">362</asp:ListItem>
                                <asp:ListItem Value="363">363</asp:ListItem>
                                <asp:ListItem Value="364">364</asp:ListItem>
                                <asp:ListItem Value="365">365</asp:ListItem>
                                <asp:ListItem Value="366">366</asp:ListItem>
                                <asp:ListItem Value="367">367</asp:ListItem>
                                <asp:ListItem Value="368">368</asp:ListItem>
                                <asp:ListItem Value="369">369</asp:ListItem>
                                <asp:ListItem Value="370">370</asp:ListItem>
                                <asp:ListItem Value="371">371</asp:ListItem>
                                <asp:ListItem Value="372">372</asp:ListItem>
                                <asp:ListItem Value="373">373</asp:ListItem>
                                <asp:ListItem Value="374">374</asp:ListItem>
                                <asp:ListItem Value="375">375</asp:ListItem>
                                <asp:ListItem Value="376">376</asp:ListItem>
                                <asp:ListItem Value="377">377</asp:ListItem>
                                <asp:ListItem Value="378">378</asp:ListItem>
                                <asp:ListItem Value="379">379</asp:ListItem>
                                <asp:ListItem Value="380">380</asp:ListItem>
                                <asp:ListItem Value="381">381</asp:ListItem>
                                <asp:ListItem Value="382">382</asp:ListItem>
                                <asp:ListItem Value="383">383</asp:ListItem>
                                <asp:ListItem Value="384">384</asp:ListItem>
                                <asp:ListItem Value="385">385</asp:ListItem>
                                <asp:ListItem Value="386">386</asp:ListItem>
                                <asp:ListItem Value="387">387</asp:ListItem>
                                <asp:ListItem Value="388">388</asp:ListItem>
                                <asp:ListItem Value="389">389</asp:ListItem>
                                <asp:ListItem Value="390">390</asp:ListItem>
                                <asp:ListItem Value="391">391</asp:ListItem>
                                <asp:ListItem Value="392">392</asp:ListItem>
                                <asp:ListItem Value="393">393</asp:ListItem>
                                <asp:ListItem Value="394">394</asp:ListItem>
                                <asp:ListItem Value="395">395</asp:ListItem>
                                <asp:ListItem Value="396">396</asp:ListItem>
                                <asp:ListItem Value="397">397</asp:ListItem>
                                <asp:ListItem Value="398">398</asp:ListItem>
                                <asp:ListItem Value="399">399</asp:ListItem>
                                <asp:ListItem Value="400">400</asp:ListItem>
                                <asp:ListItem Value="401">401</asp:ListItem>
                                <asp:ListItem Value="402">402</asp:ListItem>
                                <asp:ListItem Value="403">403</asp:ListItem>
                                <asp:ListItem Value="404">404</asp:ListItem>
                                <asp:ListItem Value="405">405</asp:ListItem>
                                <asp:ListItem Value="406">406</asp:ListItem>
                                <asp:ListItem Value="407">407</asp:ListItem>
                                <asp:ListItem Value="408">408</asp:ListItem>
                                <asp:ListItem Value="409">409</asp:ListItem>
                                <asp:ListItem Value="410">410</asp:ListItem>
                                <asp:ListItem Value="411">411</asp:ListItem>
                                <asp:ListItem Value="412">412</asp:ListItem>
                                <asp:ListItem Value="413">413</asp:ListItem>
                                <asp:ListItem Value="414">414</asp:ListItem>
                                <asp:ListItem Value="415">415</asp:ListItem>
                                <asp:ListItem Value="416">416</asp:ListItem>
                                <asp:ListItem Value="417">417</asp:ListItem>
                                <asp:ListItem Value="418">418</asp:ListItem>
                                <asp:ListItem Value="419">419</asp:ListItem>
                                <asp:ListItem Value="420">420</asp:ListItem>
                                <asp:ListItem Value="421">421</asp:ListItem>
                                <asp:ListItem Value="422">422</asp:ListItem>
                                <asp:ListItem Value="423">423</asp:ListItem>
                                <asp:ListItem Value="424">424</asp:ListItem>
                                <asp:ListItem Value="425">425</asp:ListItem>
                                <asp:ListItem Value="426">426</asp:ListItem>
                                <asp:ListItem Value="427">427</asp:ListItem>
                                <asp:ListItem Value="428">428</asp:ListItem>
                                <asp:ListItem Value="429">429</asp:ListItem>
                                <asp:ListItem Value="430">430</asp:ListItem>
                                <asp:ListItem Value="431">431</asp:ListItem>
                                <asp:ListItem Value="432">432</asp:ListItem>
                                <asp:ListItem Value="433">433</asp:ListItem>
                                <asp:ListItem Value="434">434</asp:ListItem>
                                <asp:ListItem Value="435">435</asp:ListItem>
                                <asp:ListItem Value="436">436</asp:ListItem>
                                <asp:ListItem Value="437">437</asp:ListItem>
                                <asp:ListItem Value="438">438</asp:ListItem>
                                <asp:ListItem Value="439">439</asp:ListItem>
                                <asp:ListItem Value="440">440</asp:ListItem>
                                <asp:ListItem Value="441">441</asp:ListItem>
                                <asp:ListItem Value="442">442</asp:ListItem>
                                <asp:ListItem Value="443">443</asp:ListItem>
                                <asp:ListItem Value="444">444</asp:ListItem>
                                <asp:ListItem Value="445">445</asp:ListItem>
                                <asp:ListItem Value="446">446</asp:ListItem>
                                <asp:ListItem Value="447">447</asp:ListItem>
                                <asp:ListItem Value="448">448</asp:ListItem>
                                <asp:ListItem Value="449">449</asp:ListItem>
                                <asp:ListItem Value="450">450</asp:ListItem>
                                <asp:ListItem Value="451">451</asp:ListItem>
                                <asp:ListItem Value="452">452</asp:ListItem>
                                <asp:ListItem Value="453">453</asp:ListItem>
                                <asp:ListItem Value="454">454</asp:ListItem>
                                <asp:ListItem Value="455">455</asp:ListItem>
                                <asp:ListItem Value="456">456</asp:ListItem>
                                <asp:ListItem Value="457">457</asp:ListItem>
                                <asp:ListItem Value="458">458</asp:ListItem>
                                <asp:ListItem Value="459">459</asp:ListItem>
                                <asp:ListItem Value="460">460</asp:ListItem>
                                <asp:ListItem Value="461">461</asp:ListItem>
                                <asp:ListItem Value="462">462</asp:ListItem>
                                <asp:ListItem Value="463">463</asp:ListItem>
                                <asp:ListItem Value="464">464</asp:ListItem>
                                <asp:ListItem Value="465">465</asp:ListItem>
                                <asp:ListItem Value="466">466</asp:ListItem>
                                <asp:ListItem Value="467">467</asp:ListItem>
                                <asp:ListItem Value="468">468</asp:ListItem>
                                <asp:ListItem Value="469">469</asp:ListItem>
                                <asp:ListItem Value="470">470</asp:ListItem>
                                <asp:ListItem Value="471">471</asp:ListItem>
                                <asp:ListItem Value="472">472</asp:ListItem>
                                <asp:ListItem Value="473">473</asp:ListItem>
                                <asp:ListItem Value="474">474</asp:ListItem>
                                <asp:ListItem Value="475">475</asp:ListItem>
                                <asp:ListItem Value="476">476</asp:ListItem>
                                <asp:ListItem Value="477">477</asp:ListItem>
                                <asp:ListItem Value="478">478</asp:ListItem>
                                <asp:ListItem Value="479">479</asp:ListItem>
                                <asp:ListItem Value="480">480</asp:ListItem>
                                <asp:ListItem Value="481">481</asp:ListItem>
                                <asp:ListItem Value="482">482</asp:ListItem>
                                <asp:ListItem Value="483">483</asp:ListItem>
                                <asp:ListItem Value="484">484</asp:ListItem>
                                <asp:ListItem Value="485">485</asp:ListItem>
                                <asp:ListItem Value="486">486</asp:ListItem>
                                <asp:ListItem Value="487">487</asp:ListItem>
                                <asp:ListItem Value="488">488</asp:ListItem>
                                <asp:ListItem Value="489">489</asp:ListItem>
                                <asp:ListItem Value="490">490</asp:ListItem>
                                <asp:ListItem Value="491">491</asp:ListItem>
                                <asp:ListItem Value="492">492</asp:ListItem>
                                <asp:ListItem Value="493">493</asp:ListItem>
                                <asp:ListItem Value="494">494</asp:ListItem>
                                <asp:ListItem Value="495">495</asp:ListItem>
                                <asp:ListItem Value="496">496</asp:ListItem>
                                <asp:ListItem Value="497">497</asp:ListItem>
                                <asp:ListItem Value="498">498</asp:ListItem>
                                <asp:ListItem Value="499">499</asp:ListItem>
                            </asp:DropDownList>

                        </td>
                        <td style="text-align: center;">
                            <asp:TextBox ID="TextBox2" Style="width: 100px; text-align: center;" Text='<%#AllQuery.MorePro.FormatMoney_Cart(Eval("GiaDaiLy").ToString())%>' onkeyup="valid(this,'quotes')" onblur="valid(this,'quotes')" CssClass="txt_css" runat="server" OnTextChanged="txtgia_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td style="text-align: center;">
                            <asp:TextBox ID="TextBox3" Style="width: 100px; text-align: center;" Text='<%#(Eval("Oders").ToString())%>' onkeyup="valid(this,'quotes')" onblur="valid(this,'quotes')" CssClass="txt_css" runat="server" OnTextChanged="txtthutu_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td style="text-align: center;">
                            <%#MoreAll.MoreAll.FormatDate(DataBinder.Eval(Container.DataItem, "NgayTao"))%>
                        </td>
                        <td style="text-align: center;">
                            <asp:LinkButton CssClass="active action-link-button" OnLoad="Delete_Load" CommandName="Delete" CommandArgument='<%#Eval("id") %>' ID="LinkButton2" runat="server"><i class="icon-trash"></i></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <HeaderTemplate>
                    <table cellspacing="0" style="border-collapse: collapse; margin-top: 18px" class="table table-striped table-bordered dataTable table-hover">
                        <tr class="trHeader" style="height: 25px">
                            <td style="text-align: center;" class="header">Số Lượng</td>
                            <td style="text-align: center;" class="header">Giá</td>
                            <td style="text-align: center;" class="header">Thứ tự</td>
                            <td style="text-align: center;" class="header">Ngày tạo</td>
                            <td class="header" colspan="1">Chức năng</td>
                        </tr>
                </HeaderTemplate>
                <FooterTemplate>
                    </TABLE>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>


<div style="display: none">
    <table style="border-collapse: collapse" cellpadding="0" width="100%" border="0">
        <tr height="20">
            <td></td>
        </tr>
        <tr height="25" bgcolor="whitesmoke">
            <td>
                <asp:LinkButton ID="LinkButton5" Font-Bold="true" OnClick="LinkButton4_Click" runat="server">[Thêm mới]</asp:LinkButton>
            </td>
        </tr>
    </table>
</div>

<asp:Panel ID="pn_insert" runat="server" Visible="False" Width="100%"></asp:Panel>

<div class="widget">
    <div class="widget-title">
        <h4 style="color: red; font-weight: 600"><i class="icon-list-alt"></i>&nbsp;THÊM MỚI GIÁ ĐẠI LÝ THEO SỐ LƯỢNG</h4>
    </div>

    <div class="widget-body">
        <div class='frm-add booooo'>
            <table style="border-collapse: collapse" cellpadding="0" width="100%" border="0">
                <tr>
                    <td align="right" width="175"></td>
                    <td width="10"></td>
                    <td>
                        <asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">Số Lượng Từ</td>
                    <td></td>
                    <td>
                        <asp:DropDownList ID="ddlsoluongtu" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">Số Lượng Đến</td>
                    <td></td>
                    <td>
                        <asp:DropDownList ID="ddlsoluongden" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">Giá đại lý</td>
                    <td></td>
                    <td>
                        <asp:TextBox ID="txtgia" CssClass="txt_css" ValidationGroup="GInfo" runat="server" Width="320px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="GInfo" ControlToValidate="txtgia" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">Thứ tự
                    </td>
                    <td></td>
                    <td>
                        <asp:TextBox ID="txt_order" runat="server" ValidationGroup="GInfo" CssClass="txt_css" Width="32px">1</asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="GInfo" ControlToValidate="txt_order" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right"></td>
                    <td></td>
                    <td>
                        <asp:Button ID="btn_InsertUpdate" ValidationGroup="GInfo" runat="server" CssClass="toolbar btn btn-info" OnClick="btn_InsertUpdate_Click" Text="Cập nhật" />
                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Visible="false" CssClass="toolbar btn btn-info" Text="Hủy bỏ" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>


<asp:HiddenField ID="hdimgsmall" runat="server" />
<asp:HiddenField ID="hdimgMax" runat="server" />
<asp:HiddenField ID="hdimgMaxEdit" runat="server" />
<asp:HiddenField ID="hdimgsmallEdit" runat="server" />
<asp:HiddenField ID="hdFileName" runat="server" />
<asp:HiddenField ID="hdid" runat="server" />

<input id="hd_insertupdate" value="insert" style="width: 24px; height: 22px" type="hidden" size="1" name="Hidden1" runat="server">
<input id="hd_id" style="width: 24px; height: 22px" type="hidden" size="1" name="Hidden2" runat="server">
<input id="hd_page_edit_id" style="width: 24px; height: 22px" type="hidden" size="1" name="Hidden2" runat="server">
<input id="hd_imgpath" style="width: 24px; height: 22px" type="hidden" size="1" name="Hidden2" runat="server">
<input id="hd_rootpic" style="width: 24px; height: 22px" type="hidden" size="1" runat="server">
<input id="hd_par_id" style="width: 24px; height: 22px" type="hidden" size="1" name="Hidden2" runat="server">

<asp:HiddenField ID="title_Desc" runat="server" />
<asp:HiddenField ID="hi_update" runat="server" />
<asp:HiddenField ID="hi_resources" runat="server" />
<asp:HiddenField ID="hi_Original" runat="server" />

<style>
    .frm-add.booooo {
        border: 1px solid #d7d7d7;
        background: #ebebeb;
        border-radius: 3px;
        width: 50%;
        margin: auto;
        padding: 16px;
    }

    .alerts {
        color: red;
        font-weight: bold;
    }
</style>
<script type="text/javascript">
    var r = {
        'special': /[\W]/g,
        'quotes': /[^0-9^]/g,
        'notnumbers': /[^a-zA]/g
    }
    function valid(o, w) {
        o.value = o.value.replace(r[w], '');
    }
    var substringMatcher = function (strs) {
        return function findMatches(q, cb) {
            var matches; matches = []; substrRegex = new RegExp(q, 'i'); $.each(strs, function (i, str)
            { if (substrRegex.test(str)) { matches.push(str); } }); cb(matches);
        };
    };
</script>
